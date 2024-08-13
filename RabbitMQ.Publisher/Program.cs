﻿using System.Text;
using RabbitMQ.Client;

// bağlantı oluşturma
ConnectionFactory factory = new()
{
	Uri = new Uri("amqps://ecmrwwnj:fQYvfGYSWxzgalUv5C3IePox_WwR7fIf@sparrow.rmq.cloudamqp.com/ecmrwwnj")
};

//Bağlantıyı aktifleştime ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma
channel.QueueDeclare(queue:"example-queue",exclusive:false);


// default exchange - direct exchange
for (var i = 0; i < 500; i++)
{
	await Task.Delay(100);
	var message = Encoding.UTF8.GetBytes($"Merhaba - {i}");
	channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
}

Console.Read();
