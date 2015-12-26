namespace Raven.DDD.Core.AggregateRootContext
{
    public class TestValueObject
    {
        public string From { get; protected set; }

        public TestValueObject(string caller)
        {
            From = caller;
        }
    }
}