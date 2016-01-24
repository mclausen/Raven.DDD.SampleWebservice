using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.DDD.Infrastructure.AggregateRootContext.Subscribers;
using Raven.DDD.SampleWebservice.Infrastructure;
using RavenDb.DDD.Core.Events;
using IStartable = Castle.Core.IStartable;

namespace Raven.DDD.SampleWebservice.Installers
{
    public class DomainInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<StartableFacility>();

            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<IStartable>()
                    .WithServiceBase()
                    .LifestyleSingleton(),
                
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