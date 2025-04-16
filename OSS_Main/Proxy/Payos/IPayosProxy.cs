using Net.payOS.Types;
using OSS_Main.Models.DTO.PayOS;

namespace OSS_Main.Proxy.Payos
{
    public interface IPayosProxy
    {
        Task<CreatePaymentResult> CreatePayOSPaymentUrl(PaymentData paymentData);
        Task<PaymentLinkInformation> GetPaymentLinkInfor(long id);
        Task<PaymentLinkInformation> CancelPaymentLink(long orderCode, string cancellationReason);
        PaymentResultModel ProcessReturnUrl(IQueryCollection queryParams);
    }
}
