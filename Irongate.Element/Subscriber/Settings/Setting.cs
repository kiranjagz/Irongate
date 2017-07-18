using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Subscriber.Settings
{
    public class Setting : ISetting
    {
        public string HostName { get; private set; } = "localhost";
        public string ExchangeName { get; private set; } = "irongate-inbound";
        public string RoutingKey { get; private set; } = "irongate.firemessage";
        public string QueueName { get; private set; } = "irongate-inbound-queue";
    }
}
