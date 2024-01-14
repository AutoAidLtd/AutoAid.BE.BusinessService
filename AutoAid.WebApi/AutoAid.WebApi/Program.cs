using AutoAid.WebApi.Configuration;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace AutoAid.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register appsettings.json global variables
            builder.Configuration.SettingsBinding();

            builder.Services.AddMvc();

            // Add services to the container.
            builder.Services.AddControllers();

            // Register services to DI 
            //builder.Services.AddServices();
            builder.ConfigureAutofacContainer();

            // Register FluentValidation
            builder.Services.AddFluentValidation();

            //Config endpoints router
            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.Run();
        }
    }
}