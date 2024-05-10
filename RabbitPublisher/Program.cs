using RabbitMQ.Client;
using System.Text;

namespace RabbitPublisher
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://jkspswgo:DUo0Ur4zC38A5yR7V_sBCeEtgZDaqpvy@roedeer.rmq.cloudamqp.com/jkspswgo");

            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            while (true)
            {
                channel.QueueDeclare("step-queue", true, false, false);

                var message = Console.ReadLine();
                message = $"[" + DateTime.Now.ToLongTimeString() + "] " + message;
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("", "step-queue", null, body);
            }
        }
    }
}
