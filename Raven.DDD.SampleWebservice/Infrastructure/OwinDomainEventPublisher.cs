using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using RavenDb.DDD.Core.Events;

namespace Raven.DDD.SampleWebservice.Infrastructure
{
    public class OwinDomainEventPublisher : IPublishDomainEvent
    {
        private readonly IOwinContext _context;

        public OwinDomainEventPublisher(IOwinContext context)
        {
            _context = context;
            _context.Set("domain-events", new Queue<IDomainEvent>());
        }

        public IEnumerable<IDomainEvent> CollectedEvents => _context.Get<Queue<IDomainEvent>>("domain-events");

        public async Task Publish<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            var queue =_context.Get<Queue<IDomainEvent>>("domain-events");
            await Task.Run(() => queue.Enqueue(domainEvent));
        }
    }
}