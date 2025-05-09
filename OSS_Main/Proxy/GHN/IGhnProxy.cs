using OSS_Main.Models.DTO.GHN;

namespace OSS_Main.Proxy.GHN
{
    public interface IGhnProxy
    {
        Task<string> GetProvincesAsync();
        Task<string> GetDistrictsAsync(int provinceId);
        Task<string> GetWardsAsync(int districtId);
        Task<string> GetAvailableServicesAsync(int shopId, int fromDistrictId, int toDistrictId);
        Task<string> CalculateShippingFeeAsync(int shopId, int fromDistrictId, int toDistrictId, int weight, int length, int width, int height, int serviceId, int serviceTypeId);
        Task<string> SendShippingOrderAsync(ShippingOrder order);
        Task<string> GetTokenPrintOrder(OrderRequest order);
        Task<string> PrintOrderAsync(OrderRequest order);
        Task<bool> CancelOrderOnGhnAsync(string orderCode);
        /*Task<string> GetOrderDetails(OrderRequest order);*/
        Task<GHNOrderDetails> GetOrderDetails(OrderRequest order);
    }
}
