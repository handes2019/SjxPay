using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Entities;
using SjxPayRabbitMQ;
using Volo.Abp.Modularity;

namespace SjxPay.Domain
{
    [DependsOn(typeof(RabbitmqMoudule))]
    public class DomainModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            if(configuration != null)
            {
                DB.InitAsync("SjxPayDB", MongoClientSettings.FromConnectionString(configuration.GetConnectionString("Default"))).Wait();
            }
        }
    }
}