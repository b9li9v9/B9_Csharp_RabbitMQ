using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer
{

    internal class Fanout
    {
        static void Main3(string[] args)
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
                    channel.ExchangeDeclare(exchange: "Test_Exchange_IS_Logs", type: ExchangeType.Fanout);

                    // 数据
                    var message = GetMessage(args);
                    var body = Encoding.UTF8.GetBytes(message);

                    // 推送数据
                    channel.BasicPublish(exchange: "Test_Exchange_IS_Logs",
                                        routingKey: string.Empty,
                                        basicProperties: null,
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
