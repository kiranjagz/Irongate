using Akka.Actor;
using Irongate.Element.Actors;
using Irongate.Element.Root;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irongate.Element.Subscriber;
using Irongate.Element.Subscriber.Settings;

namespace Irongate.Element.Root
{
    public class ElementRoot : IElementRoot
    {
        private ActorSystem _actorSystem;
        private IConnectionBoss _connectionBoss;
        private ISetting _setting;

        public ElementRoot(IConnectionBoss connectionBoss, ISetting setting)
        {
            _connectionBoss = connectionBoss;
            _setting = setting;
        }

        public bool Start()
        {
            _actorSystem = ActorSystem.Create("IrongateSystem");
            //_orderActor = _actorSystem.ActorOf(Props.Create(() => new GeneralActor(_connection)));

            SubscriberClient client = new SubscriberClient(_connectionBoss.Connect(), _setting);
            return true;
        }

        public bool Stop()
        {
            throw new NotImplementedException();
        }
    }
}
