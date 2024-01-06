using AutoAid.Bussiness.Configuration;
using AutoAid.Domain.Common;
using AutoAid.Infrastructure.Configuration;
using AutoAid.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

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
            });

            services.AddScoped<DbContext, AutoAidLtdContext>();
        }
    }
}
