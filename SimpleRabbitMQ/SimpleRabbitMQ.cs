using RabbitMQ.Client;
using System.Text;

namespace SimpleRabbitMQ
{
    public class SimpleRabbitMQ
    {
        public  readonly string _hostName;

        public SimpleRabbitMQ(string hostName)
        {
            _hostName = hostName;
        }
        public  bool BasicPublish(string message,string queue)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);


                channel.BasicPublish(exchange: "",
                                     routingKey: queue,
                                     basicProperties: properties,
                                     body: body);


            }
            return true;
        }
    }
}