using Akka.Actor;
using Irongate.Element.Mongo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Actors.TheTrooper
{
    public class TrooperActor : ReceiveActor
    {
        private IMongoRepository _mongoRepository;
        private const string CollectionName = "trooperpooper";

        public TrooperActor(IMongoRepository monogRepository)
        {
            _mongoRepository = monogRepository;
            Receive<TrooperMessageModel>(model => Handle_Message(model));
        }

        private void Handle_Message(TrooperMessageModel model)
        {
            var heavyCalculator = HeavyLifting(model.FireModel.FireCode);
            var randomId = new Random().Next();
            var trooperMessage = new { TrooperId = randomId, Calculation = heavyCalculator, Message = model.FireModel.Message, DeliveryTag = model.DeliveryTag };

            _mongoRepository.SaveSomething(trooperMessage, CollectionName);

            model.RabbitModel.BasicAck(model.DeliveryTag, false);

            Console.WriteLine($"Message was. {JsonConvert.SerializeObject(trooperMessage)}");
        }

        private decimal HeavyLifting(int fireCode)
        {
            var divideFirst = fireCode / 3;
            var multiple = divideFirst * 7;
            var heavyMath = multiple % 2 == 0 ? multiple + 29 : divideFirst - divideFirst + 1;
            return heavyMath;
        }
    }
}
