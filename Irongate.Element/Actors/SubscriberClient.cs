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
    public class SubscriberClient
    {
        private ConnectionFactory _connectionFactory;
        private IActorRef _orderActor;
        private IModel channelForEventing;

        public SubscriberClient(ConnectionFactory connectionFactory, IActorRef orderActor)
        {
            _connectionFactory = connectionFactory;
            _orderActor = orderActor;
            Connect();
        }

        private void Connect()
        {
            IConnection connection = _connectionFactory.CreateConnection();
            channelForEventing = connection.CreateModel();
            channelForEventing.BasicQos(0, 1, false);
            EventingBasicConsumer eventBasicConsumer = new EventingBasicConsumer(channelForEventing);
            eventBasicConsumer.Received += EventBasicConsumer_Received;
            channelForEventing.BasicConsume("Irongate-inbound", false, eventBasicConsumer);
        }

        private async void EventBasicConsumer_Received(object sender, BasicDeliverEventArgs e)
        {
            IBasicProperties basicProperties = e.BasicProperties;
            var exchange = e.Exchange;
            var contentType = basicProperties.ContentType;
            var consumerTage = e.ConsumerTag;
            var deliveryTage = e.DeliveryTag;
            var message = Encoding.UTF8.GetString(e.Body);

            //process stuff here
            var processOrder = await _orderActor.Ask<object>(message);
            //
            if (Convert.ToBoolean(processOrder))
                channelForEventing.BasicAck(e.DeliveryTag, false);
            else
                channelForEventing.BasicNack(e.DeliveryTag, false, true);
        }

    }
}
