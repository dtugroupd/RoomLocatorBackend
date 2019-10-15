using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RoomLocator.Api.Helpers;
using RoomLocator.Api.Middlewares;
using RoomLocator.Data;
using RoomLocator.Data.Config;
using RoomLocator.Data.Services;
using RoomLocator.Domain.Config;
using RoomLocator.Domain.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RoomLocator.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<RoomLocatorContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("RoomLocator")));
            services.AddSingleton(AutoMapperConfig.CreateMapper());
            services.AddHttpClient("dtu-cas")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AllowAutoRedirect = true
                });
            services.AddScoped<ValueService, ValueService>();
            services.AddScoped<MazeMapService, MazeMapService>();
            
            services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = InvalidModelHandler.HandleInvalidModelAggregate;
            });

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(o =>
            {
                o.SubstituteApiVersionInUrl = true;
                o.GroupNameFormat = "'v'VVV";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1.0",
                    Title = "Room Locator API"
                });
                c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RoomLocator.Api.xml"));
                c.DescribeAllEnumsAsStrings();
                c.DocumentFilter<LowercaseDocumentFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoomLocatorContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder =>
                {
                    builder.AllowCredentials();
                    builder.WithOrigins("http://localhost:4200");
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });

                DatabaseSeedHelper.SeedDatabase(context);
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseCors(builder =>
                {
                    builder.AllowCredentials();
                    builder.WithOrigins("https://se2-webapp04.compute.dtu.dk");
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();

            context.Database.Migrate();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Room Locator API V1"); });
        }
    }
    
    class LowercaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths;

            var newPaths = new Dictionary<string, OpenApiPathItem>();
            var removeKeys = new List<string>();
            foreach (var path in paths)
            {
                var newKey = path.Key.ToLower();
                if (newKey != path.Key)
                {
                    removeKeys.Add(path.Key);
                    newPaths.Add(newKey, path.Value);
                }
            }

            foreach (var path in newPaths)
            {
                swaggerDoc.Paths.Add(path.Key, path.Value);
            }

            foreach (var key in removeKeys)
            {
                swaggerDoc.Paths.Remove(key);
            }
        }
    }
}