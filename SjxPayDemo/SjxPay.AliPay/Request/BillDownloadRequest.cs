using SjxPay.AliPay.Domain;
using SjxPay.AliPay.Response;

namespace SjxPay.AliPay.Request
{
    public class BillDownloadRequest : BaseRequest<BillDownloadModel, BillDownloadResponse>
    {
        public BillDownloadRequest()
            : base("alipay.data.dataservice.bill.downloadurl.query")
        {
        }
    }
}
