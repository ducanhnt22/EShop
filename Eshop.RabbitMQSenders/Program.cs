using RabbitMQ.Client;
using System.Text;

Console.WriteLine("RabbitMQ Sender Started...");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    Port = 5672,
    UserName = "guest",
    Password = "12345"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "eshop.queue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

string message = "Hello from Aspire RabbitMQSender";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "",
                     routingKey: "eshop.queue",
                     basicProperties: null,
                     body: body);

Console.WriteLine($" [x] Sent: {message}");
