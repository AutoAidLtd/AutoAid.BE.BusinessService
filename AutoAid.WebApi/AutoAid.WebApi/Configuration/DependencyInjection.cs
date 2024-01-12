using AutoAid.Bussiness.Configuration;
using AutoAid.Domain.Common;
using AutoAid.Infrastructure.Configuration;
using AutoAid.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace AutoAid.WebApi.Configuration
{
    public static class DependencyInjection
    {
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

            services.AddScoped<DbContext, AutoAidLtdContext>();
        }
    }
}
