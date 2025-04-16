using OSS_Main.Models.DTO.Vnpay;

namespace OSS_Main.Proxy.vnPay
{
    public interface IVnPayProxy
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}
