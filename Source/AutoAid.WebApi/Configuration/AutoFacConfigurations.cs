using Autofac;
using Autofac.Extensions.DependencyInjection;
using Module = Autofac.Module;

namespace AutoAid.WebApi.Configuration
{
    public static class AutoFacConfigurations
    {
        public static void ConfigureAutofacContainer(this WebApplicationBuilder builder)
        {
            //Config Autofac
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //Config Autofac Container
            builder.Host.ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterModule(new AutofacModule());
            });
        }
    }

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Register Services
            builder.RegisterServices();
            base.Load(builder);
        }
    }
}
