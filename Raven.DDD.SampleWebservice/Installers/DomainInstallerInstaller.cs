using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.DDD.Infrastructure.AggregateRootContext.Subscribers;
using Raven.DDD.SampleWebservice.Infrastructure;
using RavenDb.DDD.Core.Events;

namespace Raven.DDD.SampleWebservice.Installers
{
    public class DomainInstallerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn(typeof (Command<>))
                    .WithServiceBase()
                    .LifestylePerWebRequest(),

                Classes.FromAssembly(typeof(SomeDomainEventSubscriber).Assembly)
                    .BasedOn(typeof(ISubscribeTo<>))
                    .WithServiceBase()
                    .LifestylePerWebRequest()
                );
        }
    }
}