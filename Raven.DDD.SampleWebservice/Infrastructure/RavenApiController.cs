using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Castle.Windsor;

namespace Raven.DDD.SampleWebservice.Infrastructure
{
    public class RavenApiController : ApiController
    {
        private readonly ISet<object> objToBeDisposed;

        public RavenApiController()
        {
            objToBeDisposed = new HashSet<object>();
        } 

        protected Command<TResonse> CommandFactory<TInput, TResonse>(Action<TInput> action) where TInput : Command<TResonse>
        {
            var context = HttpContext.Current.GetOwinContext();
            var container = context.Get<IWindsorContainer>("windsor-container");

            var cmd = container.Resolve<Command<TResonse>>();
            objToBeDisposed.Add(cmd);

            var input = cmd as TInput;

            if(input == null)
                throw new InvalidCastException($"Could not cast {typeof(Command<TResonse>)} to {typeof(TInput)}");


            action(input);

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