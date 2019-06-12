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
            //CreateQueueAndExchange();
        }

        public int FireMessages()
        {
            var count = 0;
 
                var channel = _connection.CreateModel();

                for (int i = 1; i <= 50; i++)
                {

                    try
                    {
                        var random = new Random().Next();
                        string fireBrand = "Fire-Jefff";
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

                        if (i % 2 == 0)
                            fireBrand = "Ice-Bobb";

                        var fireModel = new FireModel { FireBrand = fireBrand, FireCode = random, Message = $"Message from iron producer. {i}", Amount = random, DateCreated = DateTime.Now };
                        Console.WriteLine(fireModel.FireCode.ToString());
                        var message = Newtonsoft.Json.JsonConvert.SerializeObject(fireModel);
                        var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicReturn += Channel_BasicReturn;
                    channel.BasicAcks += Channel_BasicAcks;
                    channel.BasicNacks += Channel_BasicNacks;


                    channel.BasicPublish(exchange: _exchangeName,
                                             routingKey: _routingKey,
                                             basicProperties: null,
                                             body: body);

                        count++;
                        channel.ConfirmSelect();
                        channel.WaitForConfirms(TimeSpan.FromSeconds(3));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

    

            return count;
        }

        private void Channel_BasicNacks(object sender, RabbitMQ.Client.Events.BasicNackEventArgs e)
        {
            Console.WriteLine(e.DeliveryTag);
       
        }

        private void Channel_BasicAcks(object sender, RabbitMQ.Client.Events.BasicAckEventArgs e)
        {
        
            Console.WriteLine(e.DeliveryTag);
     
        }

        private void Channel_BasicReturn(object sender, RabbitMQ.Client.Events.BasicReturnEventArgs e)
        {
            var obj = e.ReplyText;
        }

        //private bool CreateQueueAndExchange()
        //{
        //    var exchangeName = _exchangeName;
        //    var queueName = _queueName;
        //    var rountingKey = _routingKey;

        //    using (var channel = _connection.CreateModel())
        //    {
        //        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);

        //        //var args = new Dictionary<string, object>();
        //        //args.Add("x-message-ttl", 60000);
        //        //channel.QueueDeclare(queueName, true, false, false, null);
        //        //channel.QueueBind(queueName, exchangeName, rountingKey);
        //    }

        //    return true;
        //}

        private class FireModel
        {
            public string FireBrand { get; internal set; }
            public int FireCode { get; internal set; }
            public string Message { get; internal set; }
            public double Amount { get; internal set; }
            public DateTime DateCreated { get; internal set; }
        }
    }
}
