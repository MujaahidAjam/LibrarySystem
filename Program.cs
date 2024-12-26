using LibrarySystem.Models;
using LibrarySystem.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LibrarySystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Configure DbContext with SQL Server provider
            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDatabase")));

            // Register services
            builder.Services.AddScoped<ILibraryService, LibraryService>(); // Registering the interface with its implementation
            builder.Services.AddScoped<ValidationService>();

            // Configure controllers with Newtonsoft.Json for circular reference handling
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            // Add Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Use HTTPS redirection
            app.UseHttpsRedirection();

            // Add authorization middleware
            app.UseAuthorization();

            // Map controller endpoints
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
