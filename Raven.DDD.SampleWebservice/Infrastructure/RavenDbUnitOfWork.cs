using System.Threading.Tasks;
using Castle.Windsor;
using Microsoft.Owin;
using Raven.Client;
using RavenDb.DDD.Core.Events;

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
            var domainEventPublisher = new OwinDomainEventPublisher(context);
            DomainEvents.Current = domainEventPublisher;

            var session = _container.Resolve<IAsyncDocumentSession>();

            await Next.Invoke(context);

            await DispatchDomainEvents(domainEventPublisher);
            
            await session.SaveChangesAsync();

            _container.Release(session);
            session.Dispose();
        }

        private async Task DispatchDomainEvents(OwinDomainEventPublisher domainEventPublisher)
        {
            foreach (var domainEvent in domainEventPublisher.CollectedEvents)
            {
                var typeParam = domainEvent.GetType();
                var type = typeof (ISubscribeTo<>).MakeGenericType(typeParam);
                var subscribers = _container.ResolveAll(type);

                foreach (var domainEventSubscriber in subscribers)
                {
                    await (Task) domainEventSubscriber
                        .GetType()
                        .GetMethod("Handle")
                        .Invoke(domainEventSubscriber, new object[] {domainEvent});

                    _container.Release(domainEventSubscriber);
                }
            }
        }
    }
}