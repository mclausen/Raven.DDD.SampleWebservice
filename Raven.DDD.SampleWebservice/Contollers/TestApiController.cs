using System;
using System.Web.Http;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Results;
using Raven.DDD.SampleWebservice.Commands;
using Raven.DDD.SampleWebservice.Infrastructure;

namespace Raven.DDD.SampleWebservice.Contollers
{
    [RoutePrefix("api")]
    public class TestApiController : RavenApiController
    {
        [Route("test")]
        [HttpGet]
        public Response Test()
        {
            return new Response() {Message = "Hello world!"};
        }

        [Route("test2")]
        [HttpGet]
        public OkResult TestA()
        {
            var response = CommandFactory<TestCommand, TestCommandResponse>(cmd =>
            {
                cmd.Message = "Hello world!";
            });

            return Ok();
        }

       

        public class Response
        {
            public string Message { get; set; }
        }
    }
}