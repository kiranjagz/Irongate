using Autofac;
using Irongate.Element.Root;
using Irongate.Element.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<IrongateElementModule>();
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var root = scope.Resolve<IElementRoot>();
                root.Start();
            }
            Console.Read();
        }
    }
}
