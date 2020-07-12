using TankGame.API.Dbcontext;
using TankGame.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using Microsoft.OpenApi.Models;
using TankGame.API.Helpers;
using System.Reflection;
using System.IO;

namespace TankGame.API
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
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? Configuration["Data:DbContext:GameConnectionString"];

            //Add PostgreSQL support

            _ = services.AddEntityFrameworkNpgsql()
                .AddDbContext<GameDbContext>(options => options.UseNpgsql(connectionString)
                , ServiceLifetime.Transient
                );

            _ = services.AddControllers(setupAction =>
              {
                  setupAction.ReturnHttpNotAcceptable = true;

              }).AddNewtonsoftJson(setupAction =>
              {
                  setupAction.SerializerSettings.ContractResolver =
                     new CamelCasePropertyNamesContractResolver();
              })
            .AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    // create a problem details object
                    var problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();
                    var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                            context.HttpContext,
                            context.ModelState);

                    // add additional info not added by default
                    problemDetails.Detail = "See the errors field for details.";
                    problemDetails.Instance = context.HttpContext.Request.Path;

                    // find out which status code to use
                    var actionExecutingContext =
                          context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                    // if there are modelstate errors & all keys were correctly
                    // found/parsed we're dealing with validation errors
                    if ((context.ModelState.ErrorCount > 0) &&
                        (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                    {
                        problemDetails.Type = "https://courselibrary.com/modelvalidationproblem";
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "One or more validation errors occurred.";

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    }

                    // if one of the keys wasn't correctly found / couldn't be parsed
                    // we're dealing with null/unparsable input
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "One or more errors on input occurred.";
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            _ = services.AddScoped(typeof(IEFGenericRepository<>), typeof(EFGenericRepository<>));
            _ = services.AddScoped<IUnitOfWork, UnitOfWork>();
            _ = services.AddScoped<ITankModelRepository, TankModelRepository>();
            _ = services.AddScoped<IGameMapRepository, GameMapRepository>();
            _ = services.AddScoped<IGameRepository, GameRepository>();
            _ = services.AddScoped<ISimulationRepository, SimulationRepository>();
            _ = services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new OpenApiInfo
                  {
                      Title = "Tank's Battle API",
                      Version = "v1",
                      Description = "A simple ASP.NET Core Web API to simulate a battle between 2 tanks on a 2D map",
                      Contact = new OpenApiContact
                      {
                          Name = "Narcis Ciorobea",
                      },

                  });
                  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                  c.IncludeXmlComments(xmlPath);
                  c.ResolveConflictingActions(apiDescriptions => apiDescriptions.ResolveActionUsingAttribute());
              });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });

            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("GetInfo", "Help");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tank's Battle Game API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
