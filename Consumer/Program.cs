using RabbitMQ.Client;
namespace Consumer { 
    internal class Program
    {
        static void Main(string[] args)
        {
            // 如果我们想连接到另一台机器上的节点，我们只需在此处指定其主机名或 IP 地址即可。
            var factory = new ConnectionFactory{HostName = "localhost"};
            using (var connection = factory.CreateConnection()) 
            {
                using (var channel = connection.CreateModel()) 
                {
                }
            
            }
        }
    }

}