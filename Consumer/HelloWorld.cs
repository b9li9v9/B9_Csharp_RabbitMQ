using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace Consumer
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


                    Console.WriteLine(" [*] Waiting for messages.");

                    var consumer = new EventingBasicConsumer(channel);


                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($" [x] Received {message}");
                        Thread.Sleep(1000);
                    };


                    channel.BasicConsume(queue: "hello",
                     autoAck: true,
                     consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();



                }

            }
        }
    }

}