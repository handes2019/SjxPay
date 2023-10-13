using MongoDB.Entities;
using SjxPay.API.Base;
using SjxPay.Core.Geteways;
using SjxPay.Core.Response;
using SjxPay.Wechatpay;
using SjxPay.Wechatpay.Domain;
using SjxPay.Wechatpay.Request;
using static MongoDB.Driver.WriteConcern;
using System.Linq.Dynamic.Core.Tokenizer;
using FastEndpoints;
using Newtonsoft.Json.Linq;
using SjxPay.Core.Utils;

namespace SjxPay.API.WebchatAPI
{
    public class Endpoint : BaseEndPoint<Request, Mapper>
    {
        private readonly IGateway _gateway;
        public Endpoint(IGateways gateways)
        {
            _gateway = gateways.Get<WechatpayGateway>();
        }

        public override void Configure()
        {
            Business = "WechatPayAPI";
            base.Configure();

        }
        public override async Task HandleAsync(Request r, CancellationToken c)
        {
            switch (r.method)
            {
                case "PublicPay": await PublicPay(r); break;
                case "AppPay": await AppPay(r); break;
                case "AppletPay": await AppletPay(r); break;
                case "WapPay": await WapPay(r); break;
                case "ScanPay": await ScanPay(r); break;
                case "BarcodePay": await BarcodePay(r); break;
                case "Query": await Query(r); break;
                case "Refund": await Refund(r); break;
                case "RefundQuery": await RefundQuery(r); break;
                case "Close": await Close(r); break;
                case "Cancel": await Cancel(r); break;
                case "Transfer": await Transfer(r); break;
                case "TransferQuery": await TransferQuery(r); break;
                case "PublicKey": await PublicKey(r); break;
                case "TransferToBank": await TransferToBank(r); break;
                case "TransferToBankQuery": await TransferToBankQuery(r); break;
                case "BillDownload": await BillDownload(r); break;
                case "FundFlowDownload": await FundFlowDownload(r); break;
                case "OAuth": await OAuth(r); break;
                default: await base.HandleAsync(r, c); break;
            }
        }
        protected async Task PublicPay(Request r)
        {
            var request = new PublicPayRequest();
            request.AddGatewayData(new PublicPayModel()
            {
                Body = r.body,
                OutTradeNo = r.out_trade_no,
                TotalAmount = r.total_amount,
                OpenId = r.open_id
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
                OutTradeNo = r.out_trade_no
            });
            var response = _gateway.Execute(request);
            await Success(response);
        }

        
        protected async Task AppletPay(Request r)
        {
            var request = new AppletPayRequest();
            request.AddGatewayData(new AppletPayModel()
            {
                Body = r.body,
                OutTradeNo = r.out_trade_no,
                TotalAmount = r.total_amount,
                OpenId = r.open_id
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
                OutTradeNo = r.out_trade_no,
                SceneInfo = r.scene_info
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
                OutTradeNo = r.out_trade_no,
                AuthCode = r.auth_code
            });
            request.PaySucceed += BarcodePay_PaySucceed;
            request.PayFailed += BarcodePay_PayFaild;
            var response = _gateway.Execute(request);
            await Success(response);
        }

        /// <summary>
        /// 支付成功事件
        /// </summary>
        /// <param name="response">返回结果</param>
        /// <param name="message">提示信息</param>
        private void BarcodePay_PaySucceed(IResponse response, string message)
        {
        }

        /// <summary>
        /// 支付失败事件
        /// </summary>
        /// <param name="response">返回结果,可能是BarcodePayResponse/QueryResponse</param>
        /// <param name="message">提示信息</param>
        private void BarcodePay_PayFaild(IResponse response, string message)
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
                RefundAmount = r.refund_amount,
                RefundDesc = r.refund_desc,
                OutRefundNo = r.out_refund_no,
                TotalAmount = r.total_amount,
                OutTradeNo = r.out_trade_no
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
                OutRefundNo = r.out_refund_no,
                RefundNo = r.refund_no
            });
            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task Close(Request r)
        {
            var request = new CloseRequest();
            request.AddGatewayData(new CloseModel()
            {
                OutTradeNo = r.out_trade_no
            });
            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task Cancel(Request r)
        {
            var request = new CancelRequest();
            request.AddGatewayData(new CancelModel()
            {
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
                OpenId = r.openid,
                Amount = r.amount,
                Desc = r.desc,
                CheckName = r.check_name,
                TrueName = r.true_name
            });
            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task TransferQuery(Request r)
        {
            var request = new TransferQueryRequest();
            request.AddGatewayData(new TransferQueryModel()
            {
                OutTradeNo = r.out_trade_no
            });
            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task PublicKey(Request r)
        {
            var request = new PublicKeyRequest();
            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task TransferToBank(Request r)
        {
            var request = new TransferToBankRequest();
            request.AddGatewayData(new TransferToBankModel()
            {
                OutTradeNo = r.out_trade_no,
                BankNo = r.bank_no,
                Amount = r.amount,
                Desc = r.desc,
                BankCode = r.bank_code,
                TrueName = r.true_name
            });
            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task TransferToBankQuery(Request r)
        {
            var request = new TransferToBankQueryRequest();
            request.AddGatewayData(new TransferToBankQueryModel()
            {
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
        protected async Task FundFlowDownload(Request r)
        {
            var request = new FundFlowDownloadRequest();
            request.AddGatewayData(new FundFlowDownloadModel()
            {
                BillDate = r.bill_date,
                AccountType = r.account_type
            });
            var response = _gateway.Execute(request);
            await Success(response);
        }
        protected async Task OAuth(Request r)
        {
            var request = new OAuthRequest();
            request.AddGatewayData(new OAuthModel()
            {
                Code = r.code
            });
            var response = _gateway.Execute(request);
            await Success(response);
        }

    }
}
