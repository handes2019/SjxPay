using MongoDB.Entities;

namespace SjxPay.Entities.BaseEntity
{
    public class ErrorDataEntity : Entity
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
