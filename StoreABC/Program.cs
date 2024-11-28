using Microsoft.EntityFrameworkCore;
using StoreABC.Businesses.Interfaces;
using StoreABC.Businesses;
using StoreABC.Models;
using StoreABC.Options;
using StoreABC.Repositories.Interfaces;
using StoreABC.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using StoreABC.HealthChecks;

namespace StoreABC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHealthChecks()
                .AddCheck<SQLHealthCheck>("SQL Express Database");

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddOptions<CustomOption>()
               .Bind(builder.Configuration.GetSection("Custom"));

            builder.Services.AddDbContext<StoreABCContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetSection("Custom")["ConnectionString"]);
            });

            builder.Services.AddScoped<IEmployeesBusiness, EmployeesBusiness>();
            builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)
                    {
                        await context.Response.WriteAsJsonAsync(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error"
                        });
                    }
                });
            });

            app.MapHealthChecks("/api/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.Run();
        }
    }
}
