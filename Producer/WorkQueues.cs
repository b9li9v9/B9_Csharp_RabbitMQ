using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer
{
    internal class WorkQueues
    {
        static void Main(string[] args)
        {
            // 如果我们想连接到另一台机器上的节点，我们只需在此处指定其主机名或 IP 地址即可。
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "test_durable_Is_True",
                        // 队列持久化
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var message = GetMessage(args);


                    var body = Encoding.UTF8.GetBytes(message);
                    // 消息持久化
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: string.Empty,
                    routingKey: "test_durable_Is_True",
                    basicProperties: properties,
                    body: body);


                    Console.WriteLine($" [x] Sent {message}");

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
