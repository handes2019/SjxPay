using FluentValidation;
using SjxPay.API.Base;

namespace SjxPay.API.WebchatAPI
{
    public class Request : BaseRequest
    {
        public string code { get; set; }
        public string bill_date { get; set; }
        public string account_type { get; set; }
        public string bill_type { get; set; }
        public string out_trade_no { get; set; }
        public string bank_no { get; set; }
        public string true_name { get; set; }
        public string bank_code { get; set; }
        public int amount { get; set; }
        public string desc { get; set; }
        public string openid { get; set; }
        public string check_name { get; set; }
        public string trade_no { get; set; }
        public string out_refund_no { get; set; }
        public string refund_no { get; set; }
        public int total_amount { get; set; }
        public string body { get; set; }
        public string open_id { get; set; }
        public string scene_info { get; set; }
        public string auth_code { get; set; }
        public int refund_amount { get; set; }
        public string refund_desc { get; set; }
    }

    public class Validator : BaseValidator<Request>
    {
        public Validator()
        {
            RuleSet("PublicPay", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.body).NotEmpty().WithMessage("body 不可为空");
                RuleFor(o => o.open_id).NotEmpty().WithMessage("open_id 不可为空");
            });

            RuleSet("AppPay", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.body).NotEmpty().WithMessage("body 不可为空");
            });
            RuleSet("AppletPay", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.body).NotEmpty().WithMessage("body 不可为空");
                RuleFor(o => o.open_id).NotEmpty().WithMessage("open_id 不可为空");
            });
            RuleSet("WapPay", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.body).NotEmpty().WithMessage("body 不可为空");
                RuleFor(o => o.scene_info).NotEmpty().WithMessage("scene_info 不可为空");
            });
            RuleSet("ScanPay", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.body).NotEmpty().WithMessage("body 不可为空");
            });
            RuleSet("BarcodePay", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.body).NotEmpty().WithMessage("body 不可为空");
                RuleFor(o => o.auth_code).NotEmpty().WithMessage("auth_code 不可为空");
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
                RuleFor(o => o.total_amount).NotEmpty().WithMessage("total_amount 不可为空");
                RuleFor(o => o.refund_amount).NotEmpty().WithMessage("refund_amount 不可为空");
                RuleFor(o => o.refund_desc).NotEmpty().WithMessage("refund_desc 不可为空");
                RuleFor(o => o.out_refund_no).NotEmpty().WithMessage("out_refund_no 不可为空");
            });
            RuleSet("RefundQuery", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.trade_no).NotEmpty().WithMessage("trade_no 不可为空");
                RuleFor(o => o.out_refund_no).NotEmpty().WithMessage("out_refund_no 不可为空");
                RuleFor(o => o.refund_no).NotEmpty().WithMessage("refund_no 不可为空");
            });
            RuleSet("Close", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
            });
            RuleSet("Cancel", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
            });
            RuleSet("Transfer", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.openid).NotEmpty().WithMessage("openid 不可为空");
                RuleFor(o => o.check_name).NotEmpty().WithMessage("check_name 不可为空");
                RuleFor(o => o.true_name).NotEmpty().WithMessage("true_name 不可为空");
                RuleFor(o => o.amount).NotEmpty().WithMessage("amount 不可为空");
                RuleFor(o => o.desc).NotEmpty().WithMessage("desc 不可为空");
            });
            RuleSet("TransferQuery", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
            });
            RuleSet("TransferQuery", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
                RuleFor(o => o.bank_no).NotEmpty().WithMessage("bank_no 不可为空");
                RuleFor(o => o.true_name).NotEmpty().WithMessage("true_name 不可为空");
                RuleFor(o => o.bank_code).NotEmpty().WithMessage("bank_code 不可为空");
                RuleFor(o => o.amount).NotEmpty().WithMessage("amount 不可为空");
                RuleFor(o => o.desc).NotEmpty().WithMessage("desc 不可为空");
            });
            RuleSet("TransferToBankQuery", () =>
            {
                RuleFor(o => o.out_trade_no).NotEmpty().WithMessage("out_trade_no 不可为空");
            });
            RuleSet("BillDownload", () =>
            {
                RuleFor(o => o.bill_date).NotEmpty().WithMessage("bill_date 不可为空");
                RuleFor(o => o.bill_type).NotEmpty().WithMessage("bill_type 不可为空");
            });
            RuleSet("FundFlowDownload", () =>
            {
                RuleFor(o => o.bill_date).NotEmpty().WithMessage("bill_date 不可为空");
                RuleFor(o => o.account_type).NotEmpty().WithMessage("account_type 不可为空");
            });
            RuleSet("OAuth", () =>
            {
                RuleFor(o => o.code).NotEmpty().WithMessage("bill_date 不可为空");
            });
        }
    }
}
