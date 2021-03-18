using System;
using System.Collections.Generic;
using RabbitMQ.Client;

namespace SplitterExample
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory() {HostName = "localhost"};

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                Splitter splitter = new Splitter();
                splitter.SetupQueues(channel);

                List<string> messages = new List<string>()
                {
                    "message one",
                    "message two",
                    "message three"
                };
            
                splitter.PublishMessages(messages);

                Console.ReadLine();
            }
        }
    }
}