using Akka.Actor;
using Irongate.Element.Mongo;
using Irongate.Element.Subsriber;
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

        public GeneralActor(IMongoRepository monogRepository)
        {
            _mongoRepository = monogRepository;
            Receive<EventModel>(model => Handle_Message(model));
        }

        private void Handle_Message(EventModel model)
        {
            var message = model.Body;
            _mongoRepository.SaveSomething(model);

            Console.WriteLine($"Message was. {model.Body}");
        }
    }
}
