namespace OSS_Main.Models.DTO.Vnpay
{
    public class PaymentResponseModel
    {
        public string OrderId { get; set; }
        public string TransactionId { get; set; }
        public string OrderResponseId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
