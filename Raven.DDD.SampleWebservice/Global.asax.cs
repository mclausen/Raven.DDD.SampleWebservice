using System;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Raven.DDD.SampleWebservice
{
    public class Global : System.Web.HttpApplication
    {

        public static IWindsorContainer Container { get; protected set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            Container = new WindsorContainer();
            Container.Install(FromAssembly.This());
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }


        protected void Application_End(object sender, EventArgs e)
        {
            Container.Dispose();
        }
    }
}