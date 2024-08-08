using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer
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

                    var severity = (args.Length > 0) ? args[0] : "info";
                    
                    // 数据
                    var message = (args.Length > 1)
                                  ? string.Join(" ", args.Skip(1).ToArray())
                                  : "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    // 启动向指定的交换器发布消息
                    channel.BasicPublish(exchange: "direct_logs",
                                         routingKey: severity,
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine($" [x] Sent '{severity}':'{message}'");

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

    }
}
