using Autofac;
using Irongate.Producer.MadProducer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Api.IoC
{
    class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Producer.MadProducer.Producer>().As<IProducer>();
            base.Load(builder);
        }
    }
}
