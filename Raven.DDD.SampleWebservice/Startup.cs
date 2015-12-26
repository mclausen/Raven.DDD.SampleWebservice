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
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.EnsureInitialized();

            app.Use<RavenDbUnitOfWork>(Global.Container);
            app.UseWebApi(httpConfiguration);
        }
    }
}
