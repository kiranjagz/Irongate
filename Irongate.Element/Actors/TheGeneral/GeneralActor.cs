using Akka.Actor;
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
        private const string CollectionName = "generalmessages";

        public GeneralActor(IMongoRepository monogRepository, IActorRef trooperActor)
        {
            _trooperActor = trooperActor;
            _mongoRepository = monogRepository;
            Receive<GeneralMessageModel>(model => Handle_Message(model));
        }

        private void Handle_Message(GeneralMessageModel model)
        {
            var message = model.EventModel.Body;
            var fireModel = JsonConvert.DeserializeObject<FireModel>(model.EventModel.Body);
            var useTrooper = IsHiddenMessage(fireModel.FireCode);

            if (useTrooper)
            {
                _trooperActor.Tell(new TrooperMessageModel(fireModel, model.RabbitModel, model.EventModel.DeliveryTag));
            }
            else
            {
                _mongoRepository.SaveSomething(model, CollectionName);
                model.RabbitModel.BasicAck(model.EventModel.DeliveryTag, false);
                Console.WriteLine($"Message was. {model.EventModel.Body}");
            }
        }

        private bool IsHiddenMessage(int fireCode) => fireCode % 2 == 0;
    }
}
