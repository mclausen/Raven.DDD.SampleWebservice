using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Raven.DDD.SampleWebservice.Commands;
using Raven.DDD.SampleWebservice.Infrastructure;

namespace Raven.DDD.SampleWebservice.Contollers
{
    [RoutePrefix("api")]
    public class TestApiController : RavenApiController
    {
        [Route("test2")]
        [HttpGet]
        public async Task<OkResult> TestA()
        {
            var response = await CommandFactory<TestCommand, TestCommandResponse>(cmd =>
            {
                cmd.Message = "Hello world!";
            }).Execute();

            

            return Ok();
        }

       

        public class Response
        {
            public string Message { get; set; }
        }
    }
}