using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Subscriber.Settings
{
    public interface ISetting
    {
        string HostName { get; }
        string ExchangeName { get; }
        string RoutingKey { get; }
        string QueueName { get; }
    }
}
