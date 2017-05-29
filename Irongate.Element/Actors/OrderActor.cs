using Akka.Actor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Actors
{
    public class OrderActor : UntypedActor
    {
        int count = 0;

        public OrderActor()
        {
        }

        protected override void OnReceive(object message)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            var bob = message;
            if (!string.IsNullOrEmpty(bob.ToString()))
            {
                count++;
                Console.WriteLine($"{DateTime.Now}.{bob.ToString()}.{count}");
                Sender.Tell(true, Self);
            }
        }
    }
}
