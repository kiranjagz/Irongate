using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Irongate.Element.Subscriber.Settings;

namespace Irongate.Element.Subscriber
{
    public class ConnectionBoss : IConnectionBoss
    {
        private ISetting _setting;
        private IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;

        //private const string _hostName = "localhost";
        //private const string _exchangeName = "irongate-inbound";
        //private const string _queueName = "irongate-inbound-queue";
        //private const string _routingKey = "irongate.firemessage";

        public ConnectionBoss(ISetting setting)
        {
            _setting = setting;
        }

        public IConnection Connect()
        {
            _connectionFactory = new ConnectionFactory() { HostName = _setting.HostName };
            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(_setting.ExchangeName, ExchangeType.Topic, true);

            //var args = new Dictionary<string, object>();
            //args.Add("x-message-ttl", 60000);
            _model.QueueDeclare(_setting.QueueName, true, false, false, null);
            _model.QueueBind(_setting.QueueName, _setting.ExchangeName, _setting.RoutingKey);
            return _connection;
        }

        public IConnection DisBand()
        {
            throw new NotImplementedException();
        }
    }
}
