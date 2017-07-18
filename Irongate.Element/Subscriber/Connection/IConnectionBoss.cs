using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Subscriber
{
    public interface IConnectionBoss
    {
        IConnection Connect();
        IConnection DisBand();
    }
}
