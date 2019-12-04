using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RoomLocator.Api.Helpers;
using RoomLocator.Api.Middlewares;
using RoomLocator.Data.Config;
using RoomLocator.Data.Hubs;
using RoomLocator.Data.Services;
using RoomLocator.Domain.Config;
using RoomLocator.Domain.Models.CredentialsModels;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddViewComponentsAsServices();
            services.AddDbContext<RoomLocatorContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("RoomLocator")));
            services.AddSingleton(AutoMapperConfig.CreateMapper());
            services.AddHttpClient("dtu-cas")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AllowAutoRedirect = true
                });
            services.AddHttpClient<CampusNetAuthService>();
            services.AddHttpClient<ModcamService>();
            services.AddSignalR();

            services.Configure<CampusNetApiCredentials>(Configuration.GetSection("Auth:CampusNet"));

            services.AddScoped<ValueService, ValueService>();
            services.AddScoped<UserService, UserService>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<SensorService, SensorService>();
            services.AddScoped<ScadadataService, ScadadataService>();
            services.AddScoped<MazeMapService, MazeMapService>();
            services.AddScoped<SurveyService, SurveyService>();
            services.AddScoped<ModcamService,ModcamService>();
            services.AddScoped<FeedbackService, FeedbackService>();
            services.AddScoped<LocalCredentialsService, LocalCredentialsService>();
            services.AddScoped<ModcamService, ModcamService>();
            services.AddScoped<CampusNetAuthService, CampusNetAuthService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<EventService,EventService>();
            
            #region JWT Setup, Anders Wiberg Olsen, s165241
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:SigningKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<UserService>();
                        var studentId = context.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                        var user = userService.GetByStudentId(studentId).GetAwaiter().GetResult();

                        if (user == null)
                        {
                            context.Fail($"User {studentId} does not exist.");
                        }

                        if (user?.Roles == null) return Task.CompletedTask;

                        var appClaims = user.Roles.Select(x => new Claim(ClaimTypes.Role, x));
                        var appIdentity = new ClaimsIdentity(appClaims);
                        context.Principal.AddIdentity(appIdentity);

                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context => {
                        var accessToken = context.Request.Query["access_token"];
                        // If the request is for our hub...
                        if (!string.IsNullOrEmpty(accessToken) &&
                            context.Request.Path.Value.StartsWith("/api/socket")) {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization();

            #endregion
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
                    builder.WithExposedHeaders("Content-Disposition");
                });
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
                    builder.WithExposedHeaders("Content-Disposition");
                });
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSignalR(routes => { routes.MapHub<MainHub>("/api/socket"); });
            app.UseMvc();
            
            context.Database.Migrate();
            DatabaseSeedHelper.SeedRoles(context);
            DatabaseSeedHelper.SeedSensors(context);
            DatabaseSeedHelper.SeedMazeMapSections(context);
            if (env.IsDevelopment())
            {
                DatabaseSeedHelper.SeedDemoSurveys(context);
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Room Locator API V1"); });
        }
    }
    
    /// <summary>
    /// Use to lowercase some of the swagger documentation automatically
    /// </summary>
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