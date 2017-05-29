using Akka.Actor;
using Irongate.Element.Actors;
using Irongate.Element.Root;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Root
{
    public class ElementRoot : IElementRoot
    {
        private ActorSystem _actorSystem;
        private IActorRef _orderActor;

        private ConnectionFactory _connectionFactory;

        public ElementRoot()
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
        }

        public bool Start()
        {
            var isCreated = CreateQueueAndExchange();
            if (!isCreated)
                throw new Exception("Rabbit failure");

            _actorSystem = ActorSystem.Create("IrongateSystem");

            _orderActor = _actorSystem.ActorOf(Props.Create(() => new OrderActor()));
            SubscriberClient client = new SubscriberClient(_connectionFactory, _orderActor);
            return true;
        }

        public bool Stop()
        {
            throw new NotImplementedException();
        }

        public bool CreateQueueAndExchange()
        {
            bool isCreated;
            var exchangeName = "Irongate-inbound";
            var queueName = "Irongate-inbound";
            var rountingKey = "#.createorder";

            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true);

                    channel.QueueDeclare(queueName, true, false, false, null);

                    channel.QueueBind(queueName, exchangeName, rountingKey);

                    isCreated = true;
                }
            }

            return isCreated;
        }
    }
}
