using Akka.Actor;
using Akka.Routing;
using Irongate.Element.Actors.TheTrooper;
using Irongate.Element.Mongo;
using Irongate.Element.Subsriber;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Actors.TheGeneral
{
    //Does high level jobs, secret jobs done by trooper
    public class GeneralActor : ReceiveActor
    {
        private IMongoRepository _mongoRepository;
        private IActorRef _trooperActor;
        private const string _collectionName = "generalmessages";
        private const string _trooperActorName = "trooperActor";

        public GeneralActor(IMongoRepository monogRepository)
        {
            _mongoRepository = monogRepository;
            var trooperProp = Props.Create<TrooperActor>(monogRepository).WithRouter(new RoundRobinPool(5));
            _trooperActor = Context.ActorOf(trooperProp, _trooperActorName);
            Receive<GeneralMessageModel>(model => Handle_Message(model));
        }

        private void Handle_Message(GeneralMessageModel model)
        {
            var message = model.EventModel.Body;
            var fireModel = JsonConvert.DeserializeObject<FireModel>(model.EventModel.Body);
            var useTrooper = IsHiddenMessage(fireModel.FireCode);

            if (useTrooper)
                _trooperActor.Tell(new TrooperMessageModel(fireModel, model.RabbitModel, model.EventModel.DeliveryTag));
            else
            {
                _mongoRepository.SaveSomething(model, _collectionName);
                model.RabbitModel.BasicAck(model.EventModel.DeliveryTag, false);
                Console.WriteLine($"Message was. {model.EventModel.Body}");
            }
        }

        private bool IsHiddenMessage(int fireCode) => fireCode % 2 == 0;

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(// or AllForOneStrategy
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromSeconds(30),
                localOnlyDecider: x =>
                {
                    // Maybe ArithmeticException is not application critical
                    // so we just ignore the error and keep going.
                    if (x is ArithmeticException) return Directive.Resume;

                    // Error that we have no idea what to do with
                    else if (x is IndexOutOfRangeException) return Directive.Escalate;

                    // Error that we can't recover from, stop the failing child
                    else if (x is NotSupportedException) return Directive.Stop;

                    // otherwise restart the failing child
                    else return Directive.Restart;
                });
        }
    }
}
