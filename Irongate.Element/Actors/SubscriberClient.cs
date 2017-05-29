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
    public class SubscriberClient
    {
        private IActorRef _orderActor;

        private IConnection _connection;
        private IModel _model;

        public SubscriberClient(IConnection connection, IActorRef orderActor)
        {
            _connection = connection;
            _orderActor = orderActor;
            Connect();
        }

        private void Connect()
        {
            _model = _connection.CreateModel();
            _model.BasicQos(0, 1, false);

            EventingBasicConsumer eventBasicConsumer = new EventingBasicConsumer(_model);
            eventBasicConsumer.Received += EventBasicConsumer_Received;
            _model.BasicConsume("Irongate-inbound", false, eventBasicConsumer);
        }

        private void EventBasicConsumer_Received(object sender, BasicDeliverEventArgs e)
        {
            IBasicProperties basicProperties = e.BasicProperties;

            var eventModel = new EventModel()
            {
                Body = e.Body,
                ConsumerTag = e.ConsumerTag,
                DeliveryTag = e.DeliveryTag,
                Exchange = e.Exchange,
                RoutingKey = e.RoutingKey,
                Redelivered = e.Redelivered,
                ContentType = e.BasicProperties.ContentType
            };

            //process stuff here
            //var processOrder = await _orderActor.Ask<object>(message);
            _orderActor.Tell(eventModel);
            //
            //if (Convert.ToBoolean(processOrder))
            //    _model.BasicAck(e.DeliveryTag, false);
        }

    }
}
