using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Models
{
    public class EventModel
    {
        public byte[] Body { get; set; }
        public string ConsumerTag { get; set; }
        public ulong DeliveryTag { get; set; }
        public string Exchange { get; set; }
        public bool Redelivered { get; set; }
        public string RoutingKey { get; set; }
        public string ContentType { get; set; }
    }
}
