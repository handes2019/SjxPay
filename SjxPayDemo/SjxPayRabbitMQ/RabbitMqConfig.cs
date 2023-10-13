using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjxPayRabbitMQ
{
    public class RabbitMqConfig
    {
        public static string hostname { get; set; }
        public static string clientname { get; set; }
        public static string exchangename { get; set; }
        public static async void LoadConfig(IConfiguration config)
        {
            var rabbitmq = config.GetSection("RabbitMQ");
            var connection = rabbitmq.GetSection("Connections");
            var defaultoption = connection.GetSection("Default");
            hostname = defaultoption.GetSection("HostName").Value;

            var eventsbus = rabbitmq.GetSection("EventBus");
            clientname = eventsbus.GetSection("ClientName").Value;
            exchangename = eventsbus.GetSection("ExchangeName").Value;
        }
    }
}
