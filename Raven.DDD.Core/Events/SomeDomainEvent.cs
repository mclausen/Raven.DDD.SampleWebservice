using RavenDb.DDD.Core.Events;

namespace Raven.DDD.Core.Events
{
    public class SomeDomainEvent : IDomainEvent
    {
        public string EventProperty { get; set; }
    }
}
