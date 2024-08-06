using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace Producer
{
    internal class HelloWorld
    {
        static void _Main(string[] args)
        {
            // 如果我们想连接到另一台机器上的节点，我们只需在此处指定其主机名或 IP 地址即可。
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    const string message = "Hello World!";

                    for (int i = 0; i < 100000; i++)
                    {

                        string messageaddi = $"[{i}]: "+message;
                        var body = Encoding.UTF8.GetBytes(messageaddi);

                        channel.BasicPublish(exchange: string.Empty,
                        routingKey: "hello",
                        basicProperties: null,
                        body: body);
                    }

                    Console.WriteLine($" [x] Sent {message}");

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}