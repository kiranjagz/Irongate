using Akka.Actor;
using Irongate.Element.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Actors
{
    public class OrderActor : ReceiveActor
    {
        int count = 0;

        private IConnection _connection;
        private IModel _model;

        public OrderActor(IConnection connection)
        {
            _connection = connection;
            Receive<EventModel>(model => Handle_Message(model));
            Connect();
        }

        private void Handle_Message(EventModel model)
        {
            var message = Encoding.UTF8.GetString(model.Body);

            count++;
            Console.WriteLine($"{DateTime.Now}.{message.ToString()}.{count}");

            _model.BasicAck(model.DeliveryTag, false);
        }

        private void Connect()
        {
            _model = _connection.CreateModel();
        }

        //protected override void OnReceive(object message)
        //{
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    var bob = message;
        //    if (!string.IsNullOrEmpty(bob.ToString()))
        //    {
        //        count++;
        //        Console.WriteLine($"{DateTime.Now}.{bob.ToString()}.{count}");
        //        Sender.Tell(true, Self);
        //    }
        //}
    }
}
