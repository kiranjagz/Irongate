using Irongate.Producer.MadProducer;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Producer
{
    class Program
    {
        private static IConnectionFactory _connectionFactory;
        private static IConnection _connection;
        private static IProducer _fireProducer;

        static void Main(string[] args)
        {
            _connectionFactory = new ConnectionFactory { HostName = "localhost" };
            _connection = _connectionFactory.CreateConnection();
            _fireProducer = new MadProducer.Producer(_connection);

            var count = _fireProducer.FireMessages();
            Console.WriteLine($"Number of messsages sent to the irongate. {count}");

            Console.Read();
        }
    }
}
