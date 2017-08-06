using Autofac;
using Irongate.Element.Root;
using Irongate.Element.Subscriber;
using Irongate.Element.Subscriber.Settings;
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
            builder.RegisterType<ConnectionBoss>().As<IConnectionBoss>();
            builder.RegisterType<Setting>().As<ISetting>();
            builder.RegisterType<Mongo.MongoSetting>().As<Mongo.IMongoSetting>();
            builder.RegisterType<Mongo.MongoRepository>().As<Mongo.IMongoRepository>();

            base.Load(builder);
        }
    }
}
