using AutoAid.Application.Firebase;
using AutoAid.Application.Repository;
using AutoAid.Infrastructure.Firebase;
using AutoAid.Infrastructure.Repository;
using Autofac;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAid.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFirebaseClient, FirebaseClient>();
        }

        public static void RegisterInfrastructureServices(this ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<FirebaseClient>().As<IFirebaseClient>().InstancePerLifetimeScope();
        }
    }

}
