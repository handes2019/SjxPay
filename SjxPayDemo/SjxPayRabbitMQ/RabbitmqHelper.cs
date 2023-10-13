using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SjxPay.Common;
using System.Collections.Concurrent;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace SjxPayRabbitMQ
{
    public class RabbitmqHelper: ISingletonDependency
    {
        private readonly ConnectionFactory connectionFactory = new ConnectionFactory { HostName = RabbitMqConfig.hostname };
        private IConnection? connection {  get; set; }
        private IModel? channel { get; set; }
        private ulong sequenceNumber { get; set; }
        private ConcurrentDictionary<ulong,string> outstandingConfirms {  get; set; }
        private IBasicProperties props { get; set; }

        private string replyQueueName { get; set; }
        private EventingBasicConsumer consumer { get; set; }
        private string queueName { get; set; }
        private ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper { get; set; }

        public RabbitmqHelper()
        {
            connectionFactory.RequestedHeartbeat = TimeSpan.FromSeconds(60);
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: RabbitMqConfig.exchangename, type: ExchangeType.Topic);
            channel.ConfirmSelect();
            //发布前可以通过Channel#NextPublishSeqNo获取序列号：
            sequenceNumber = channel.NextPublishSeqNo;
            replyQueueName = channel.QueueDeclare().QueueName;
            channel.BasicAcks += Channel_BasicAcks;
            channel.BasicNacks += Channel_BasicNacks;

            queueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: queueName,
                     autoAck: true,
                     consumer: consumer);
            consumer.Received += Consumer_Received;
            
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            if (!callbackMapper.TryRemove(e.BasicProperties.CorrelationId, out var tcs))
                return;
            var body = e.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            tcs.TrySetResult(response);
        }

        private void Channel_BasicNacks(object? sender, RabbitMQ.Client.Events.BasicNackEventArgs e)
        {
            outstandingConfirms.TryGetValue(e.DeliveryTag, out string body);
            Log4netHelper.Info($"Message with body {body} has been nack. Sequence number: {e.DeliveryTag}, multiple: {e.Multiple}");
            CleanOutstandingConfirms(e.DeliveryTag, e.Multiple);
        }

        private void Channel_BasicAcks(object? sender, RabbitMQ.Client.Events.BasicAckEventArgs e)
        {
            outstandingConfirms.TryGetValue(e.DeliveryTag, out string body);
            Log4netHelper.Info($"Message with body {body} has been nack-ed. Sequence number: {e.DeliveryTag}, multiple: {e.Multiple}");
            CleanOutstandingConfirms(e.DeliveryTag, e.Multiple);
        }

        private void CleanOutstandingConfirms(ulong sequenceNumber,bool multiple)
        {
            if (multiple)
            {
                var confirmed = outstandingConfirms.Where(k=>k.Key <= sequenceNumber);
                foreach (var entry in confirmed)
                {
                    outstandingConfirms.TryRemove(entry.Key, out _);
                }
            }
            else
            {
                outstandingConfirms.TryRemove(sequenceNumber, out _);
            }
        }

        public void Publish(string routingKey,string message)
        {
            outstandingConfirms.TryAdd(channel.NextPublishSeqNo, message);
            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: RabbitMqConfig.exchangename,
                         routingKey: routingKey,
                         basicProperties: props,
                         body: body);
            Log4netHelper.Info($" [x] Sent '{routingKey}':'{message}'");
        }

        public void AppPublish(string message)
        {
            Publish("*.app",message);
        }

        public void SetRoutingKeytReceived(string routingKey)
        {
            
            channel.QueueBind(queue: queueName,
                   exchange: RabbitMqConfig.exchangename,
                   routingKey: routingKey);
        }



    }
}
