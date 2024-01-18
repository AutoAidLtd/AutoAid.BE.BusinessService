using AutoAid.Application.Service.Common;
using AutoAid.Bussiness.Service;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AutoAid.Bussiness.Configuration
{
    public static class DependenncyInjection
    {
        public static void AddBussinessServices(this IServiceCollection services)
        {
            services.AddScoped<IPlaceService, PlaceService>();
        }

        public static void RegisterBussinessServices(this ContainerBuilder builder)
        {
            // register for services
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            builder.RegisterType<JWTTokenService>()
                .As<ITokenService>().InstancePerLifetimeScope();
            
        }
    }
}
