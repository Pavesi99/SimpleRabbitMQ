﻿using RabbitMQ.Client;
using System.Text;

static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
}

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{

    var message = GetMessage(args);
    var body = Encoding.UTF8.GetBytes(message);

    var properties = channel.CreateBasicProperties();
    properties.Persistent = true;
    channel.QueueDeclare(queue: "task_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);


    channel.BasicPublish(exchange: "",
                         routingKey: "task_queue",
                         basicProperties: properties,
                         body: body);


}