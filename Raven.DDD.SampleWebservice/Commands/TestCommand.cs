using System.Threading.Tasks;
using Raven.Client;
using Raven.DDD.SampleWebservice.Infrastructure;

namespace Raven.DDD.SampleWebservice.Commands
{
    public class TestCommand : Command<TestCommandResponse>
    {
        public string Message { get; set; }

        public TestCommand(IAsyncDocumentSession session) : base(session){}

        public async override Task<TestCommandResponse> Execute()
        {
            var testCommandResponse = new TestCommandResponse() {Message = Message };
            await Session.StoreAsync(testCommandResponse);

            return testCommandResponse;
        }
    }

    public class TestCommandResponse
    {
        public string Message { get; set; }
    }
}