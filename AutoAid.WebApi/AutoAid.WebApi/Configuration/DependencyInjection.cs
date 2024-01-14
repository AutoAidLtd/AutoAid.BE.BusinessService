using AutoAid.Bussiness.Configuration;
using AutoAid.Domain.Common;
using AutoAid.Infrastructure.Configuration;
using AutoAid.Infrastructure.DbContexts;
using Autofac;
using Google;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace AutoAid.WebApi.Configuration
{
    public static class DependencyInjection
    {
        // register services for Microsoft DI
        public static void AddServices(this IServiceCollection services)
        {
            services.AddDbContext();
            services.AddInfrastructureServices();
            services.AddBussinessServices();
        }

        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<AutoAidLtdContext>(options =>
            {
                options.UseNpgsql(AppConfig.ConnectionStrings.DefaultConnection)
                       .UseSnakeCaseNamingConvention();
            }, contextLifetime: ServiceLifetime.Scoped);

            //services.AddScoped<DbContext, AutoAidLtdContext>();
        }

        // register services for autofac
        public static void RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterDbContext();
            builder.RegisterInfrastructureServices();
            builder.RegisterBussinessServices();
        }

        public static void RegisterDbContext(this ContainerBuilder builder)
        {
            builder.Register(c => new NpgsqlConnection(AppConfig.ConnectionStrings.DefaultConnection))
                   .As<IDbConnection>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<AutoAidLtdContext>().As<DbContext>().InstancePerLifetimeScope();
        }
    }
}
