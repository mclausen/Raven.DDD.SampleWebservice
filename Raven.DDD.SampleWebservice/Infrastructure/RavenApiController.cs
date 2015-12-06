using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Castle.Windsor;
using Microsoft.Owin;

namespace Raven.DDD.SampleWebservice.Infrastructure
{
    public class RavenApiController : ApiController
    {
        private ISet<object> objToBeDisposed;

        public RavenApiController()
        {
            objToBeDisposed = new HashSet<object>();
        } 

        protected Command<TResonse> CommandFactory<TInput, TResonse>(Action<TInput> action) where TInput : Command<TResonse>
        {
            var context = HttpContext.Current.GetOwinContext();
            var container = context.Get<IWindsorContainer>("windsor-container");

            var cmd = container.Resolve<TInput>();
            objToBeDisposed.Add(cmd);

            action(cmd);

            return cmd;
        }

        protected override void Dispose(bool disposing)
        {
            var context = HttpContext.Current.GetOwinContext();
            var container = context.Get<IWindsorContainer>("windsor-container");

            foreach (var obj in objToBeDisposed)
            {
                container.Release(obj);
            }

            base.Dispose(disposing);
        }
    }
}