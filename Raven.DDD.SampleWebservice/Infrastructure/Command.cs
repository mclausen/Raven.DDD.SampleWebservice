using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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