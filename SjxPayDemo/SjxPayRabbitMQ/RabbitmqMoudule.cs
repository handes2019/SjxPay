using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace SjxPayRabbitMQ
{
    public class RabbitmqMoudule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var config = context.Services.GetConfiguration();
            RabbitMqConfig.LoadConfig(config);
        }
    }
}