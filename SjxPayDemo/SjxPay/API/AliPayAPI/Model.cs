using FluentValidation;
using SjxPay.API.Base;

namespace SjxPay.API.AliPayAPI
{
    public class Request : BaseRequest
    {
        public string code { get; set; }
        public string bill_date { get; set; }
        public string bill_type { get; set; }
        public string out_trade_no { get; set; }
        public int amount { get; set; }
        public string trade_no { get; set; }
        public int total_amount { get; set; }
        public string body { get; set; }
        public string auth_code { get; set; }
        public int refund_amount { get; set; }
        public string subject { get;set; }
        public string refund_reason {  get; set; }  
        public string out_request_no { get; set; }
        public string payee_account { get; set; }
        public string remark { get; set; }
        public string payee_type { get; set; }
    }

    public class Validator : BaseValidator<Request>
    {
        public Validator()
        {
            RuleSet("WebPay", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.subject).NotEmpty().WithMessage("subject 不可为空");
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.body).NotEmpty().WithMessage("body 不可为空");
            });
            RuleSet("WapPay", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.subject).NotEmpty().WithMessage("subject 不可为空");
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.body).NotEmpty().WithMessage("body 不可为空");
            });
            RuleSet("AppPay", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.subject).NotEmpty().WithMessage("subject 不可为空");
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.body).NotEmpty().WithMessage("body 不可为空");
            });
            RuleSet("ScanPay", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.subject).NotEmpty().WithMessage("subject 不可为空");
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.body).NotEmpty().WithMessage("body 不可为空");
            });
            RuleSet("BarcodePay", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.auth_code).NotEmpty().WithMessage("auth_code 不可为空");
                RuleFor(o => o.subject).NotEmpty().WithMessage("subject 不可为空");
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.body).NotEmpty().WithMessage("body 不可为空");
            });
            RuleSet("Query", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.trade_no).NotEmpty().WithMessage("trade_no 不可为空");
            });
            RuleSet("Refund", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.trade_no).NotEmpty().WithMessage("trade_no 不可为空");
                RuleFor(o => o.refund_amount).NotEmpty().WithMessage("refund_amount 不可为空");
                RuleFor(o => o.refund_reason).NotEmpty().WithMessage("refund_reason 不可为空");
                RuleFor(o => o.out_request_no).NotEmpty().WithMessage("out_request_no 不可为空");
            });
            RuleSet("RefundQuery", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.trade_no).NotEmpty().WithMessage("trade_no 不可为空");
                RuleFor(o => o.out_request_no).NotEmpty().WithMessage("out_request_no 不可为空");
            });
            RuleSet("Cancel", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.trade_no).NotEmpty().WithMessage("trade_no 不可为空");
            });
            RuleSet("Close", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.trade_no).NotEmpty().WithMessage("trade_no 不可为空");
            });
            RuleSet("Transfer", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.payee_account).NotEmpty().WithMessage("payee_account 不可为空");
                RuleFor(o => o.payee_type).NotEmpty().WithMessage("payee_type 不可为空");
                RuleFor(o => o.amount).NotEmpty().WithMessage("amount 不可为空");
                RuleFor(o => o.remark).NotEmpty().WithMessage("remark 不可为空");
            });
            RuleSet("TransferQuery", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.trade_no).NotEmpty().WithMessage("trade_no 不可为空");
            });
            RuleSet("BillDownload", () =>
            {
                RuleFor(o => o.bill_date).NotEmpty().WithMessage("bill_date 不可为空");
                RuleFor(o => o.bill_type).NotEmpty().WithMessage("bill_type 不可为空");
            }); 
        }
    }
}
