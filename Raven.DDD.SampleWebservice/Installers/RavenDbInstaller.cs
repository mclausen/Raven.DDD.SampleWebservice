using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.DDD.Infrastructure.Indexes;

namespace Raven.DDD.SampleWebservice.Installers
{
    public class RavenDbInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDocumentStore>()
                .UsingFactoryMethod(CreateDocumentStore)
                .LifestyleSingleton()
                .OnDestroy(x => x.Dispose()));

            container.Register(Component.For<IAsyncDocumentSession>()
                .UsingFactoryMethod(CreateSession)
                .LifestylePerWebRequest());
        }

        private IDocumentStore CreateDocumentStore()
        {
            var documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "RavenDDDStore"
            };
            
            documentStore.Initialize(ensureDatabaseExists: true);
            IndexCreation.CreateIndexes(typeof (TestAggregateRootIndex).Assembly, documentStore);

            return documentStore;
        }

        private IAsyncDocumentSession CreateSession(IKernel input)
        {
            var store = input.Resolve<IDocumentStore>();
            var session = store.OpenAsyncSession();
            return session;
        }
    }
}