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
using Irongate.Element.Actors.TheGeneral;
using Irongate.Element.Mongo;
using Irongate.Element.Actors.TheTrooper;

namespace Irongate.Element.Root
{
    public class ElementRoot : IElementRoot
    {
        private ActorSystem _actorSystem;
        private IActorRef _generalActor;
        private IConnectionBoss _connectionBoss;
        private IMongoRepository _mongoRepository;
        private ISetting _setting;

        public ElementRoot(IConnectionBoss connectionBoss, ISetting setting, IMongoRepository mongoRepository)
        {
            _connectionBoss = connectionBoss;
            _setting = setting;
            _mongoRepository = mongoRepository;
        }

        public bool Start()
        {
            _actorSystem = ActorSystem.Create("IrongateSystem");
            _generalActor = _actorSystem.ActorOf(Props.Create(() => new GeneralActor(_mongoRepository)),"GeneralValdez");
            SubscriberClient client = new SubscriberClient(_connectionBoss.Connect(), _setting, _generalActor);
            return true;
        }

        public bool Stop()
        {
            throw new NotImplementedException();
        }
    }
}
