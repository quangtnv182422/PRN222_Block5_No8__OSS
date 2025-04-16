using Net.payOS;
using Net.payOS.Types;
using OSS_Main.Models.DTO.PayOS;

namespace OSS_Main.Proxy.Payos
{
    public class PayosProxy : IPayosProxy
    {

        private readonly PayOS payOS;

        public PayosProxy(IConfiguration configuration)
        {
            var clientId = configuration["PayOS:ClientID"];
            var apiKey = configuration["PayOS:APIKey"];
            var checksumKey = configuration["PayOS:ChecksumKey"];

            payOS = new PayOS(clientId, apiKey, checksumKey);

        }

        public async Task<CreatePaymentResult> CreatePayOSPaymentUrl(PaymentData paymentData)
        {
            return await payOS.createPaymentLink(paymentData);
        }

        public async Task<PaymentLinkInformation> GetPaymentLinkInfor(long id)
        {
            return await payOS.getPaymentLinkInformation(id);
        }

        public async Task<PaymentLinkInformation> CancelPaymentLink(long orderCode, string cancellationReason)
        {
            return await payOS.cancelPaymentLink(orderCode, cancellationReason);
        }

        public PaymentResultModel ProcessReturnUrl(IQueryCollection queryParams)
        {
            var result = new PaymentResultModel
            {
                Code = queryParams.ContainsKey("code") ? queryParams["code"] : string.Empty,
                Id = queryParams.ContainsKey("id") ? queryParams["id"] : string.Empty,
                Cancel = queryParams.ContainsKey("cancel") ? queryParams["cancel"] : string.Empty,
                Status = queryParams.ContainsKey("status") ? queryParams["status"] : string.Empty,
                OrderCode = queryParams.ContainsKey("orderCode") ? queryParams["orderCode"] : string.Empty,
            };
            return result;
        }
    }
}
