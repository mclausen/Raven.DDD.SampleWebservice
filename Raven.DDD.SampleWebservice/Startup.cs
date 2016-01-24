using System.Threading;
using System.Web.Http;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Owin;
using Microsoft.Owin.BuilderProperties;
using Owin;
using Raven.DDD.SampleWebservice.Infrastructure;

[assembly: OwinStartup(typeof(Raven.DDD.SampleWebservice.Startup))]

namespace Raven.DDD.SampleWebservice
{
    public class Startup
    {
        public static IWindsorContainer Container { get; protected set; }

        public void Configuration(IAppBuilder app)
        {
            Container = new WindsorContainer();
            Container.Install(FromAssembly.This());

            var properties = new AppProperties(app.Properties);
            var cancellationToken = properties.OnAppDisposing;
            if (cancellationToken != CancellationToken.None)
            {
                cancellationToken.Register(() =>
                {
                    Container.Dispose();
                });
            }

            SetupOwinPipeline(app);
        }

        private static void SetupOwinPipeline(IAppBuilder app)
        {
            app.Use<RavenDbUnitOfWork>(Container);

            var httpConfiguration = CreateHttpConfiguration();
            app.UseWebApi(httpConfiguration);
        }

        private static HttpConfiguration CreateHttpConfiguration()
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.EnsureInitialized();
            return httpConfiguration;
        }
    }
}
