using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StudentTracking.Application.Main.Profiles;
using System;
using System.IO;
using System.Reflection;

namespace SchoolWepAPI.App_Start
{
    public class ProfileInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(GetExecutingDirectory()))
                    .BasedOn<ProfileBase>()
                    .WithService.Base()
                    .LifestyleTransient());

            foreach (var profile in container.ResolveAll<ProfileBase>())
            {
                Mapper.Configuration.AddProfile(profile);
            }
        }
        protected string GetExecutingDirectory()
        {
            var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            return Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
        }
    }
}