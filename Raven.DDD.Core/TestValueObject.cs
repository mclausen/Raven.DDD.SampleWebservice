namespace Raven.DDD.Core
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