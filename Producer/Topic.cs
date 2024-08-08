using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Producer
{
    internal class Topic
    {

            static void Main4(string[] args)
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
                        channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

                        // 路由键
                        var routingKey = (args.Length > 0) ? args[0] : "anonymous.info";
                        
                        // 数据
                        var message = (args.Length > 1)
                                      ? string.Join(" ", args.Skip(1).ToArray())
                                      : "Hello World!";
                        var body = Encoding.UTF8.GetBytes(message);
                        
                        // 启动
                        channel.BasicPublish(exchange: "topic_logs",
                                             routingKey: routingKey,
                                             basicProperties: null,
                                             body: body);

                        Console.WriteLine($" [x] Sent '{routingKey}':'{message}'");
                    }
                }
            }

    }
}
