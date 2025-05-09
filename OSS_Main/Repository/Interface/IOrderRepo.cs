using OSS_Main.Models.Entity;

namespace OSS_Main.Repository.Interface
{
    public interface IOrderRepo
    {
        Task CreateOrderAsync(Order order);
        Task UpdateOrderOnGHNAsync(Order order);
        Task<Order> GetOrderByIdAsync(string orderId);
        Task<List<Order>> GetOrderByCustomerIdAsync(string customerId);
        Task<List<Order>> GetAllOrderAsync();
        Task<List<Order>> GetAllOrderShippingAsync();
        Task<OrderStatus> GetOrderStatusByNameAsync(string orderStatusName);
        Task<List<Order>> GetAllOrderByUserReceiverAsync(string userId);
        Task<List<OrderStatus>> GetAllOrderStatusAsync();
        Task<decimal> GetTotalCostsAsync();
        Task<Dictionary<int, decimal>> GetTotalCostsByMonthAsync(int year);
        Task<List<int>> GetAllOrderYearsAsync();
    }
}
