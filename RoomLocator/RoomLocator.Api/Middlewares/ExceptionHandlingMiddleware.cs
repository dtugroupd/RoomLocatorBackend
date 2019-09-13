using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RoomLocator.Domain.ViewModels;
using Shared;

namespace RoomLocator.Api.Middlewares
{
public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        private readonly IDictionary<Type, HttpStatusCode> _exceptionCodes = new Dictionary<Type, HttpStatusCode> {
            { typeof(NotFoundException), HttpStatusCode.NotFound },
//            { typeof(DuplicationException), HttpStatusCode.Conflict },
            { typeof(InvalidRequestException), HttpStatusCode.BadRequest },
            { typeof(Exception), HttpStatusCode.InternalServerError },
        };

        // TODO: Move this to a helper class
        private readonly JsonSerializer _jsonSerializer = new JsonSerializer {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private HttpStatusCode GetStatusCode(Type type)
            => _exceptionCodes.ContainsKey(type) ? _exceptionCodes[type] : HttpStatusCode.InternalServerError;

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError($"Failed with: {exception.Message}\n{exception.StackTrace}");
            if (exception.InnerException != null)
            {
                _logger.LogError($"Inner Exception: {exception.InnerException.Message}\n{exception.InnerException.StackTrace}");
            }
            var title = "Unexpected Error";
            var message =
                "Oops! Sorry! Something went wrong. Please context support@consensus.dk so we can try to fix it.";
            var statusCode = HttpStatusCode.InternalServerError;

            ErrorViewModel error = null;
            MultipleErrorsViewModel errors = null;

            if (exception is AggregateException aex)
            {
                if (aex.InnerExceptions.Count == 1)
                {
                    return HandleExceptionAsync(context, aex.InnerExceptions[0]);
                }
                else if (aex.InnerExceptions.Count > 1)
                {
                    foreach (var ex in aex.InnerExceptions)
                    {
                        statusCode = GetStatusCode(ex.GetType());
                        _logger.LogError($"{statusCode}: {ex.Message}\n{ex.StackTrace}");
                    }
                    errors = new MultipleErrorsViewModel(aex.InnerExceptions);
                }
            }
            else
            {
                statusCode = GetStatusCode(exception.GetType());
                if (exception is BaseException bex)
                {
                    title = bex.Title;
                    message = bex.Message;
                    _logger.LogError($"{statusCode}: {bex.Title}: {bex.Message}\n{bex.StackTrace}");
                }
            }

            error = new ErrorViewModel(title, message);


            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(
                errors != null ?
                    JObject.FromObject(errors, _jsonSerializer).ToString() :
                    JObject.FromObject(error, _jsonSerializer).ToString()
            );
        }
    }
}