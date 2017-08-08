using Irongate.Element.Actors.TheGeneral;
using Irongate.Element.Subsriber;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Actors.TheTrooper
{
    public class TrooperMessageModel
    {
        public FireModel FireModel { get; set; }
        public IModel RabbitModel { get; set; }
        public ulong DeliveryTag { get; set; }

        public TrooperMessageModel(FireModel fireModel, IModel rabbitModel, ulong deliveryTag)
        {
            FireModel = fireModel;
            RabbitModel = rabbitModel;
            DeliveryTag = deliveryTag;
        }
    }
}
