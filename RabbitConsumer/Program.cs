using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitConsumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://jkspswgo:DUo0Ur4zC38A5yR7V_sBCeEtgZDaqpvy@roedeer.rmq.cloudamqp.com/jkspswgo");

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("step-queue", true, false, false);

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("step-queue", true, consumer);

            consumer.Received += consumerReceived;

            Console.ReadLine();
        }

        private static void consumerReceived(object? sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine(message);
        }
    }
}
