using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    internal class Direct
    {
        static void Main(string[] args)
        {
            // 连接
            // 如果我们想连接到另一台机器上的节点，我们只需在此处指定其主机名或 IP 地址即可。
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                // 创建通道
                using (var channel = connection.CreateModel())
                {

                    // 交换器
                    channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

                    // 队列
                    var queueName = channel.QueueDeclare().QueueName;

                    if (args.Length < 1)
                    {
                        Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
                                                Environment.GetCommandLineArgs()[0]);
                        Console.WriteLine(" Press [enter] to exit.");
                        Console.ReadLine();
                        Environment.ExitCode = 1;
                        return;
                    }

                    // 队列绑定
                    foreach (var severity in args)
                    {
                        channel.QueueBind(queue: queueName,
                                          exchange: "direct_logs",
                                          routingKey: severity);
                    }

                    Console.WriteLine(" [*] Waiting for messages.");

                    // 消费者处理函数
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var routingKey = ea.RoutingKey;
                        Console.WriteLine($" [x] Received '{routingKey}':'{message}'");
                    };

                    // 启动消费
                    channel.BasicConsume(queue: queueName,
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
