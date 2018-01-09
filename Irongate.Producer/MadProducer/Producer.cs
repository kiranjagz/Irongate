using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Producer.MadProducer
{
    public class Producer : IProducer
    {
        private IConnection _connection;
        private IConnectionFactory _connectionFactory;

        private const string _exchangeName = "irongate-inbound";
        private const string _queueName = "irongate-inbound-queue";
        private const string _routingKey = "irongate.firemessage";

        public Producer(IConnection connection)
        {
            _connection = connection;
            CreateQueueAndExchange();
        }

        public int FireMessages()
        {
            var count = 0;
            try
            {
                using (var channel = _connection.CreateModel())
                {
                    for (int i = 1; i <= 200; i++)
                    {
                        
                        var random = new Random().Next();
                        string fireBrand = "Fire-Jefff";
                        System.Threading.Thread.Sleep(20);

                        if (i % 2 == 0)
                            fireBrand = "Ice-Bobb";

                        var fireModel = new FireModel { FireBrand = fireBrand, FireCode = random, Message = $"Message from iron producer. {i}" };
                        Console.WriteLine(fireModel.FireCode.ToString());
                        var message = Newtonsoft.Json.JsonConvert.SerializeObject(fireModel);
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: _exchangeName,
                                             routingKey: _routingKey,
                                             basicProperties: null,
                                             body: body);
                        count++;
                    }
                }
            }
            catch(Exception e) { Console.WriteLine(e.Message); }

            return count;
        }

        private bool CreateQueueAndExchange()
        {
            var exchangeName = _exchangeName;
            var queueName = _queueName;
            var rountingKey = _routingKey;

            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true);

                //var args = new Dictionary<string, object>();
                //args.Add("x-message-ttl", 60000);
                channel.QueueDeclare(queueName, true, false, false,null);
                channel.QueueBind(queueName, exchangeName, rountingKey);
            }

            return true;
        }

        private class FireModel
        {
            public string FireBrand { get; internal set; }
            public int FireCode { get; internal set; }
            public string Message { get; internal set; }
        }
    }
}
