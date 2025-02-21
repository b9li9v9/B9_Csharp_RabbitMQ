﻿using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    internal class WorkQueues
    {
        static void Main2(string[] args)
        {
            // 如果我们想连接到另一台机器上的节点，我们只需在此处指定其主机名或 IP 地址即可。
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "test_durable_Is_True",
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

                    // 这告诉 RabbitMQ 不要一次向工作线程提供多条消息。换言之，在工作人员处理并确认前一条消息之前，不要将新消息分派给工作人员。
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);


                    Console.WriteLine(" [*] Waiting for messages.");

                    var consumer = new EventingBasicConsumer(channel);


                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($" [x] Received {message}");


                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);

                        Console.WriteLine(" [x] Done");
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };


                    channel.BasicConsume(queue: "test_durable_Is_True",
                     autoAck: false,
                     consumer: consumer);


                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }

            }
        }

    }
}

