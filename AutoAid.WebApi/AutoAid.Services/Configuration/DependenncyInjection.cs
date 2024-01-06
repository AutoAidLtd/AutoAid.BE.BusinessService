using AutoAid.Application.Service;
using AutoAid.Bussiness.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAid.Bussiness.Configuration
{
    public static class DependenncyInjection
    {
        public static void AddBussinessServices(this IServiceCollection services)
        {
            services.AddScoped<IPlaceService, PlaceService>();
        }
    }
}
