using System.Threading.Tasks;
using Raven.Client;
using Raven.DDD.Core.AggregateRootContext;
using Raven.DDD.SampleWebservice.Infrastructure;

namespace Raven.DDD.SampleWebservice.Commands
{
    public class TestCommand : Command<TestCommandResponse>
    {
        public string Message { get; set; }

        public TestCommand(IAsyncDocumentSession session) : base(session){}

        public async override Task<TestCommandResponse> Execute()
        {
            var testRoot = await Session.LoadAsync<TestAggregateRoot>(TestAggregateRoot.TestId);
            if (testRoot == null)
            {
                testRoot = new TestAggregateRoot();
                await Session.StoreAsync(testRoot);
            }

            await testRoot.DomainMehtod(Message);

            var testCommandResponse = new TestCommandResponse {Message = "Great succes" };
            return testCommandResponse;
        }
    }

    public class TestCommandResponse
    {
        public string Message { get; set; }
    }
}