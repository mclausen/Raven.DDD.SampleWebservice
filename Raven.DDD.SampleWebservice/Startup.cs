using System.Web.Http;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Owin;
using Owin;
using Raven.DDD.SampleWebservice.Infrastructure;

[assembly: OwinStartup(typeof(Raven.DDD.SampleWebservice.Startup))]

namespace Raven.DDD.SampleWebservice
{
    public class Startup
    {
        private IWindsorContainer _container;

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());

            app.Use<RavenDbUnitOfWork>(_container);

            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.EnsureInitialized();

            app.UseWebApi(httpConfiguration);
        }
    }
}
