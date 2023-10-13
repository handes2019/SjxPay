namespace SjxPay.API.NotifyAPI
{
    public class Mapper : RequestMapper<Request, SampleEntity>
    {
        public override SampleEntity ToEntity(Request r)
        {
            return new SampleEntity()
            {
                Id = r.ID,
                Name = r.Name
            };
        }
    }
}
