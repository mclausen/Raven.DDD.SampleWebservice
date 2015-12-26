using System.Threading.Tasks;
using Raven.Client;

namespace Raven.DDD.SampleWebservice.Infrastructure
{
    public abstract class Command<T>
    {
        protected readonly IAsyncDocumentSession Session;

        protected Command(IAsyncDocumentSession session)
        {
            Session = session;
        }

        public abstract Task<T> Execute();
    }
}