using Akka.Actor;
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
    public class GeneralActor : ReceiveActor
    {
        public GeneralActor()
        {
            Receive<EventModel>(model => Handle_Message(model));
        }

        private void Handle_Message(EventModel model)
        {
            //var message = Encoding.UTF8.GetString(model.Body);
        }
    }
}
