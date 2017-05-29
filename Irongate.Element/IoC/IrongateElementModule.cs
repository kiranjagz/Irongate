using Autofac;
using Irongate.Element.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.IoC
{
    public class IrongateElementModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ElementRoot>().As<IElementRoot>();
            base.Load(builder);
        }
    }
}
