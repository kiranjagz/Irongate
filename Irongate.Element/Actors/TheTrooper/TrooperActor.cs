using Akka.Actor;
using Irongate.Element.Mongo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Actors.TheTrooper
{
    public class TrooperActor : ReceiveActor
    {
        private IMongoRepository _mongoRepository;
        private const string CollectionName = "trooperpooper";
        private static int count = 0;
        private static List<SummaryDateMessageModel> _summaryList;

        public TrooperActor(IMongoRepository monogRepository)
        {
            _mongoRepository = monogRepository;
            _summaryList = new List<SummaryDateMessageModel>();
            Receive<TrooperMessageModel>(model => Handle_Message(model));
            Receive<WriteFileMessageModel>(model => WriteFile(model));
            Receive<List<SummaryDateMessageModel>>(model => Handle_Summary(model));
        }

        private void Handle_Summary(List<SummaryDateMessageModel> model)
        {
            List<double> diff = new List<double>();
            model.ForEach(x =>
            {
                var minDIff = x.DateReceived.Subtract(x.DateCreated).TotalMinutes;
                Console.WriteLine($"Diff in object: {x.FireCode} is > {minDIff}");
                diff.Add(minDIff);
            });

            var average = diff.Average();
            Console.WriteLine($"Average is: {average}");
        }

        private void WriteFile(WriteFileMessageModel model)
        {
            using (StreamWriter writer = new StreamWriter("c:\\newtrooper.txt", true))
            {
                writer.WriteLine($"{DateTime.Now}-{model.Firecode}-{model.Message}");
            }
        }

        private async void Handle_Message(TrooperMessageModel model)
        {
            count++;
            var summary = new SummaryDateMessageModel
            {
                DateCreated = model.FireModel.DateCreated,
                DateReceived = model.FireModel.DateReceived,
                FireCode = model.FireModel.FireCode
            };
            _summaryList.Add(summary);
            if (count == 10)
            {
                Self.Tell(_summaryList);
                _summaryList = new List<SummaryDateMessageModel>();
                count = 0;
            }

            var randomId = new Random().Next();
            var trooperMessage = new { TrooperId = randomId, Message = model.FireModel.Message, model.FireModel.DateCreated, model.FireModel.Amount, DeliveryTag = model.DeliveryTag };
            var isSaved = await _mongoRepository.SaveSomething(trooperMessage, CollectionName);
            Self.Tell(new WriteFileMessageModel(model.FireModel.FireCode, model.FireModel.Message));
            model.RabbitModel.BasicAck(model.DeliveryTag, false);
            //Console.WriteLine($"Message was. {JsonConvert.SerializeObject(trooperMessage)}");
        }
    }
}
