namespace SjxPay.API.AliPayAPI
{
    public class Mapper : RequestMapper<Request, SampleEntity>
    {
        public override SampleEntity ToEntity(Request r)
        {
            return new SampleEntity()
            {
                Id = r.ID,
                
            };
        }
    }
}
