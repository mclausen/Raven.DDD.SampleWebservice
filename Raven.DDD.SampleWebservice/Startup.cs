using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Raven.DDD.SampleWebservice.Infrastructure;

[assembly: OwinStartup(typeof(Raven.DDD.SampleWebservice.Startup))]

namespace Raven.DDD.SampleWebservice
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            app.Use<RavenDbUnitOfWork>(Global.Container);

            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.EnsureInitialized();

            app.UseWebApi(httpConfiguration);
        }
    }
}
