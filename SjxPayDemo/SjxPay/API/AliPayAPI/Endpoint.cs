using SjxPay.AliPay;
using SjxPay.AliPay.Domain;
using SjxPay.AliPay.Request;
using SjxPay.API.Base;
using SjxPay.Core.Geteways;
using static MongoDB.Driver.WriteConcern;
using YamlDotNet.Core;

namespace SjxPay.API.AliPayAPI
{
    public class Endpoint : BaseEndPoint<Request, Mapper>
    {
        private readonly IGateway _gateway;
        public Endpoint(IGateways gateways)
        {
            _gateway = gateways.Get<AlipayGateway>();
        }

        public override void Configure()
        {
            Business = "AliPayAPI";
            base.Configure();
            AllowAnonymous();
        }
        public override async Task HandleAsync(Request r, CancellationToken c)
        {
            switch (r.method)
            {
                case "WebPay": await WebPay(r); break;
                case "WapPay": await WapPay(r); break;
                case "AppPay": await AppPay(r); break;
                case "ScanPay": await ScanPay(r); break;
                case "BarcodePay": await BarcodePay(r); break;
                case "Query": await Query(r); break;
                case "Refund": await Refund(r); break;
                case "RefundQuery": await RefundQuery(r); break;
                case "Cancel": await Cancel(r); break;
                case "Close": await Close(r); break;
                case "Transfer": await Transfer(r); break;
                case "TransferQuery": await TransferQuery(r); break;
                case "BillDownload": await BillDownload(r); break;
                default: await base.HandleAsync(r, c); break;
            }
        }
        protected async Task WebPay(Request r)
        {
            var request = new WebPayRequest();
            request.AddGatewayData(new WebPayModel()
            {
                Body = r.body,
                TotalAmount = r.total_amount,
                Subject = r.subject,
                OutTradeNo = r.out_trade_no
            });

            var response = _gateway.Execute(request);
            await Success(response);
        }

        protected async Task WapPay(Request r)
        {
            var request = new WapPayRequest();
            request.AddGatewayData(new WapPayModel()
            {
                Body = r.body,
                TotalAmount = r.total_amount,
                Subject = r.subject,
                OutTradeNo = r.out_trade_no
            });
            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task AppPay(Request r)
        {
            var request = new AppPayRequest();
            request.AddGatewayData(new AppPayModel()
            {
                Body = r.body,
                TotalAmount = r.total_amount,
                Subject = r.subject,
                OutTradeNo = r.out_trade_no
            });

            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task ScanPay(Request r)
        {
            var request = new ScanPayRequest();
            request.AddGatewayData(new ScanPayModel()
            {
                Body = r.body,
                TotalAmount = r.total_amount,
                Subject = r.subject,
                OutTradeNo = r.out_trade_no
            });

            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task BarcodePay(Request r)
        {
            var request = new BarcodePayRequest();
            request.AddGatewayData(new BarcodePayModel()
            {
                Body = r.body,
                TotalAmount = r.total_amount,
                Subject = r.subject,
                OutTradeNo = r.out_trade_no,
                AuthCode = r.auth_code
            });
            request.PaySucceed += Request_PaySucceed;
            request.PayFailed += Request_PayFailed;

            var response = _gateway.Execute(request);
            await Success(response);
        }
        /// <summary>
        /// 支付成功事件
        /// </summary>
        /// <param name="arg1">返回结果</param>
        /// <param name="arg2">提示信息</param>
        private void Request_PayFailed(Core.Response.IResponse arg1, string arg2)
        {
            
        }
        /// <summary>
        /// 支付失败事件
        /// </summary>
        /// <param name="arg1">返回结果,可能是BarcodePayResponse/QueryResponse</param>
        /// <param name="arg2">提示信息</param>
        private void Request_PaySucceed(Core.Response.IResponse arg1, string arg2)
        {
            
        }
        protected async Task Query(Request r)
        {
            var request = new QueryRequest();
            request.AddGatewayData(new QueryModel()
            {
                TradeNo = r.trade_no,
                OutTradeNo = r.out_trade_no
            });

            var response = _gateway.Execute(request);
            await Success(response);
        }

        protected async Task Refund(Request r)
        {
            var request = new RefundRequest();
            request.AddGatewayData(new RefundModel()
            {
                TradeNo = r.trade_no,
                OutTradeNo = r.out_trade_no,
                RefundAmount = r.refund_amount,
                RefundReason = r.refund_reason,
                OutRefundNo = r.out_request_no
            });

            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task RefundQuery(Request r)
        {
            var request = new RefundQueryRequest();
            request.AddGatewayData(new RefundQueryModel()
            {
                TradeNo = r.trade_no,
                OutTradeNo = r.out_trade_no,
                OutRefundNo = r.out_request_no
            });

            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task Cancel(Request r)
        {
            var request = new CancelRequest();
            request.AddGatewayData(new CancelModel()
            {
                TradeNo = r.trade_no,
                OutTradeNo = r.out_trade_no
            });

            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task Close(Request r)
        {
            var request = new CloseRequest();
            request.AddGatewayData(new CloseModel()
            {
                TradeNo = r.trade_no,
                OutTradeNo = r.out_trade_no
            });

            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task Transfer(Request r)
        {
            var request = new TransferRequest();
            request.AddGatewayData(new TransferModel()
            {
                OutTradeNo = r.out_trade_no,
                PayeeAccount = r.payee_account,
                Amount = r.amount,
                Remark = r.remark,
                PayeeType = r.payee_type
            });

            var response = _gateway.Execute(request);
            await Success(response);
        }

        protected async Task TransferQuery(Request r)
        {
            var request = new TransferQueryRequest();
            request.AddGatewayData(new TransferQueryModel()
            {
                TradeNo = r.trade_no,
                OutTradeNo = r.out_trade_no
            });

            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task BillDownload(Request r)
        {
            var request = new BillDownloadRequest();
            request.AddGatewayData(new BillDownloadModel()
            {
                BillDate = r.bill_date,
                BillType = r.bill_type
            });

            var response = _gateway.Execute(request);
            await Success(response);
        }
    }
}
