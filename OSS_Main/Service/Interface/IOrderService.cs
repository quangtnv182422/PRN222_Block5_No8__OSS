using OSS_Main.Models.Entity;

namespace OSS_Main.Service.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string? customerId,string paymentMethod,  List<int> cartItemIds,float totalCost, int orderStatus, string? note, int receiverId, string orderId_GHN);
        Task UpdateOrderOnGHNAsync(Order order);
        Task<Order> GetOrderByIdAsync(string orderId);
        Task<List<Order>> GetOrderByCustomerIdAsync(string customerId);
        Task<List<Order>> GetAllOrderAsync();
        Task<List<Order>> GetAllOrderShippingAsync();
        Task<OrderStatus> GetOrderStatusByNameAsync(string orderStatusName);
        Task<List<Order>> GetAllOrderByUserReceiverAsync(string userId);
        Task<List<OrderStatus>> GetAllOrderStatusAsync();
    }

}
