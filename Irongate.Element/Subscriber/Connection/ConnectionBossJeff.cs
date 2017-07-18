using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Subscriber.Connection
{
    public class ConnectionBossJeff
    {
        public IConnection Connection { get; set; }
        public string QueueName { get; set; }
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
    }
}
