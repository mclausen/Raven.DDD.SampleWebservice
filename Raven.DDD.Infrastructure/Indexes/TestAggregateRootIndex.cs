using System.Linq;
using Raven.Client.Indexes;
using Raven.DDD.Core.AggregateRootContext;

namespace Raven.DDD.Infrastructure.Indexes
{
    public class TestAggregateRootIndex : AbstractIndexCreationTask<TestAggregateRoot>
    {
        public TestAggregateRootIndex()
        {
            Map = testAggregateRoots => from root in testAggregateRoots
                                        select new
                                        {
                                            ID = root.Id
                                        };
        }
    }
}
