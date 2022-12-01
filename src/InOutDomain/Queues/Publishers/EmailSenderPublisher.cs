using InOut.Domain.Queues.Publishers.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace InOut.Domain.Queues.Publishers
{
    public class EmailSenderPublisher : IEmailSenderPublisher
    {
        const string BROKER_NAME = "email_sender";

        public void Publish(string json)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: BROKER_NAME,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                     routingKey: BROKER_NAME,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
