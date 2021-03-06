﻿using System.Diagnostics;
using System.Threading.Tasks;
using Raven.DDD.Core.AggregateRootContext.Events;
using RavenDb.DDD.Core.Events;

namespace Raven.DDD.Infrastructure.AggregateRootContext.Subscribers
{
    public class SomeDomainEventSubscriber : ISubscribeTo<SomeDomainEvent>
    {
        public async Task Handle(SomeDomainEvent domainEvent)
        {
            Trace.Write("Hello from the other side!");
        }
    }
}
