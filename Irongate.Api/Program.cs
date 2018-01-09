using Autofac;
using Autofac.Integration.WebApi;
using Irongate.Api.IoC;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using System.Web.Http;
using Irongate.Producer.MadProducer;
using Irongate.Api.Controllers;

namespace Irongate.Api
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            //builder.RegisterModule<IoCModule>();
            builder.RegisterType<Producer.MadProducer.Producer>().As<IProducer>();
            builder.RegisterApiControllers(typeof(RabbitController).Assembly);
            Container = builder.Build();

            var webApiResolver = new AutofacWebApiDependencyResolver(Container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
            string baseUri = "http://localhost:8082";

            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(baseUri);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
            Console.ReadLine();
        }
    }
}
