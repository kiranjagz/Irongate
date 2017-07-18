using Irongate.Element.Subsriber;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Subscriber
{
    public static class ExtensionMethods
    {
        public static EventModel Map(this BasicDeliverEventArgs e)
        {
            return new EventModel()
            {
                Body = Encoding.UTF8.GetString(e.Body),
                ConsumerTag = e.ConsumerTag,
                DeliveryTag = e.DeliveryTag,
                Exchange = e.Exchange,
                RoutingKey = e.RoutingKey,
                Redelivered = e.Redelivered,
                ContentType = e.BasicProperties.ContentType
            };
        }
    }
}
