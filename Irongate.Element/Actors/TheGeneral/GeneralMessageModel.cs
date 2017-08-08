using Irongate.Element.Subsriber;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Actors.TheGeneral
{
    public class GeneralMessageModel
    {
        public EventModel EventModel { get; set; }
        public IModel RabbitModel { get; set; }

        public GeneralMessageModel(EventModel eventModel, IModel rabbitModel)
        {
            EventModel = eventModel;
            RabbitModel = rabbitModel;
        }
    }
}
