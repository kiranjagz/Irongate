using Akka.Actor;
using Irongate.Element.Subscriber.Settings;
using Irongate.Element.Subsriber;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Subscriber
{
    public class SubscriberClient
    {
        private IActorRef _generalActor;

        private ISetting _setting;

        private IConnection _connection;
        private IModel _model;
        EventingBasicConsumer _eventBasicConsumer;

        public SubscriberClient(IConnection connection, ISetting setting, IActorRef generalActor)
        {
            _generalActor = generalActor;
            _setting = setting;
            _connection = connection;
            _model = _connection.CreateModel();
            _model.BasicQos(0, 1, false);
            _eventBasicConsumer = new EventingBasicConsumer(_model);
            _eventBasicConsumer.Received += EventBasicConsumer_Received;
            _model.BasicConsume(_setting.QueueName, false, _eventBasicConsumer);
        }

        private void EventBasicConsumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventModel = e.Map();

            _generalActor.Tell(eventModel);

            _model.BasicAck(e.DeliveryTag, false);
        }
    }
}
