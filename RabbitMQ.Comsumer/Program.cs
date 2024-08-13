using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


// bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://htralqyl:DEkQJLbqTh_dQ_1hN_TrmxpljloTfIn3@cow.rmq2.cloudamqp.com/htralqyl");

//bağlantı aktifleştirme kanal açma
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//Queue oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);


//Queue den mesaj okuma

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: "example-queue",autoAck:true,consumer);
consumer.Received += (sender, e) =>
{
	Console.WriteLine(Encoding.UTF8.GetString(e.Body.ToArray()));
};

Console.Read();