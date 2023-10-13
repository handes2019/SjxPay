using SjxPay.API.Base;

namespace SjxPay.API.SampleAPI
{
    public class Endpoint : BaseEndPoint<Request, Mapper>
    {
        public override void Configure()
        {
            Business = "Sample";
            base.Configure();

        }
        public override async Task HandleAsync(Request r, CancellationToken c)
        {
            switch (r.method)
            {
                case "GET": await GetMethod(r); break;
                case "publisher": await RabbitMqPublisher(r); break;
                case "subscriber": await RabbitMqSubscriber(); break;
                default: await base.HandleAsync(r, c); break;
            }
        }
        protected async override Task TestEndPoint(Request r)
        {

        }
        private async Task GetMethod(Request r)
        {

            var entity = Map.ToEntity(r);
            await Success(entity);
        }

        private async Task RabbitMqPublisher(Request r)
        {
            await Success(null);
        }

        private async Task RabbitMqSubscriber()
        {
            await Success(null);
        }


    }
}
