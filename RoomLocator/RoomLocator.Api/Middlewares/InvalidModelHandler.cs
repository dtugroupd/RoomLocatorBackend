using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace RoomLocator.Api.Middlewares
{
    public static class InvalidModelHandler
    {
        public static IActionResult HandleInvalidModelAggregate(ActionContext context)
        {
            var modelState = context.ModelState;
            var exceptions = new List<Exception>();
			
            foreach (var (_, value) in modelState) {
                exceptions.AddRange(value.Errors.Select(x => new InvalidRequestException("Invalid model state", x.ErrorMessage)));
            }
			
            throw new AggregateException(exceptions);
        }
    }
}