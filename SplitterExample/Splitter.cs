using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace SplitterExample
{
    public class Splitter
    {
        private IModel _channel;
        private const string ExchangeName = "splitter_example";
        private const string RoutingKey = "receiverChannel";

        public void SetupQueues(IModel channel)
        {
            _channel = channel;
            channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
            channel.QueueDeclare(durable: true);
            
            
        }

        public void PublishMessages(List<string> messages)
        {
            foreach (string message in messages)
            {
                PublishMessage(message);
            }
        }

        private  void PublishMessage(string message)
        {
            byte[] body = Encoding.UTF8.GetBytes(message);
            
            _channel.BasicPublish(
                exchange:ExchangeName,
                routingKey: RoutingKey,
                basicProperties: null,
                body: body);
        }
    }
}