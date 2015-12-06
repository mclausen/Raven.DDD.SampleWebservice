using System.Threading.Tasks;
using Castle.Windsor;
using Microsoft.Owin;
using Raven.Client;

namespace Raven.DDD.SampleWebservice.Infrastructure
{
    public class RavenDbUnitOfWork : OwinMiddleware
    {
        private readonly IWindsorContainer _container;

        public RavenDbUnitOfWork(OwinMiddleware next, IWindsorContainer container) : base(next)
        {
            _container = container;
        }

        public async override Task Invoke(IOwinContext context)
        {
            context.Set("windsor-container", _container);

            var session = _container.Resolve<IAsyncDocumentSession>();

            await Next.Invoke(context);

            await session.SaveChangesAsync();
            session.Dispose();
        }
    }
}