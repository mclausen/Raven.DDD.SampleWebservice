using System.Threading.Tasks;
using Raven.DDD.Core.Events;
using RavenDb.DDD.Core;
using RavenDb.DDD.Core.Events;

namespace Raven.DDD.Core
{
    public class TestAggregateRoot : AggregateRoot
    {
        public const string TestId = "TestRoot/1";

        public TestAggregateRoot()
        {
            Id = TestId;
        }

        public TestValueObject ValueObject { get; protected set; }

        public async Task DomainMehtod(string caller)
        {
            ValueObject = new TestValueObject(caller);

            await DomainEvents.Publish(new SomeDomainEvent());
        }
    }
}
