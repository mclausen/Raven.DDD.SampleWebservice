using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.DDD.SampleWebservice.Infrastructure;

namespace Raven.DDD.SampleWebservice.Installers
{
    public class CommandInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn(typeof (Command<>))
                .WithServiceBase()
                .LifestyleTransient()
                );
        }
    }
}