using System.Collections.Generic;
using Autofac;
using Business.Impl.Services;
using Business.Models;
using Business.Services;

namespace ZeroToOverkill.IoC
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<InMemoryGroupService>().As<IGroupService>().SingleInstance();
        }
    }
}
