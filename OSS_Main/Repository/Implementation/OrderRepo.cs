using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;

namespace OSS_Main.Repository.Implementation
{
    public class OrderRepo : IOrderRepo
    {
        private readonly Prn222ProjectContext _context;
        public OrderRepo(Prn222ProjectContext context)
        {
            _context = context;
        }
        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderOnGHNAsync(Order order)
        {
            _context.Orders.Update(order);
             await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
            return await _context.Orders
                .Include(x => x.Receiver)
                .Include(x => x.OrderItemOrders)
                .ThenInclude(x => x.CartItem)
                .ThenInclude(x => x.ProductSpec)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.OrderId.ToString() == orderId);
        }

        public async Task<List<Order>> GetOrderByCustomerIdAsync(string customerId)
        {
            return await _context.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.Receiver)
                .Include(x => x.OrderItemOrders)
                .ThenInclude(x => x.CartItem)
                .ThenInclude(x => x.ProductSpec)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductImages)
                .Where(x => x.CustomerId.ToString() == customerId)
                //.OrderByDescending(x => x.OrderAt)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrderAsync()
        {
            return await _context.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.Receiver)
                .Include(x => x.OrderItemOrders)
                .ThenInclude(x => x.CartItem)
                .ThenInclude(x => x.ProductSpec)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductImages)
                //.OrderByDescending(x => x.OrderAt)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrderShippingAsync()
        {
            return await _context.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.Receiver)
                .Include(x => x.OrderItemOrders)
                    .ThenInclude(x => x.CartItem)
                    .ThenInclude(x => x.ProductSpec)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.ProductImages)
                .Where(x => x.OrderStatusId >= 2 && x.OrderStatusId <= 26
                            && x.OrderStatusId != 6 // đây là status cancel
                            && x.OrderStatusId != 3 // đây là status confirm_received
                            && x.OrderStatusId != 14 // đây là status delivered
                            && x.OrderStatusId != 26) // đây là status confirm_returned
                .ToListAsync();
        }

        public async Task<OrderStatus> GetOrderStatusByNameAsync(string orderStatusName)
        {
            return await _context.OrderStatuses
                .Where(x => x.OrderStatusName.Equals(orderStatusName))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetAllOrderByUserReceiverAsync(string userId)
        {
            return await _context.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.Receiver)
                .Include(x => x.OrderItemOrders)
                .ThenInclude(x => x.CartItem)
                .ThenInclude(x => x.ProductSpec)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductImages)
                .Where(x => x.Receiver.CustomerId.Equals(userId))
                .ToListAsync();
        }
        public async Task<List<OrderStatus>> GetAllOrderStatusAsync()
        {
            return await _context.OrderStatuses
                .ToListAsync();
        }

        public async Task<decimal> GetTotalCostsAsync() => await _context.Orders.SumAsync(o => o.TotalCost);

        public async Task<Dictionary<int, decimal>> GetTotalCostsByMonthAsync(int year)
        {
            var monthlyRevenue = await _context.Orders.Where(o => o.OrderAt.Year == year && (o.OrderStatusId == 14 || o.OrderStatusId == 3))
                                        .GroupBy(o => o.OrderAt.Month)
                                        .Select(o => new
                                        {
                                            Month = o.Key,
                                            Revenue = o.Sum(or => or.TotalCost)
                                        })
                                        .OrderBy(result => result.Month)
                                        .ToListAsync();
            Dictionary<int, decimal> mappedMonthRevenue = new Dictionary<int, decimal>();
            foreach (var item in monthlyRevenue)
            {
                mappedMonthRevenue.Add(item.Month, item.Revenue);
            }

            return mappedMonthRevenue;
        }

        public async Task<List<int>> GetAllOrderYearsAsync()
        {
            return await _context.Orders.Where(o => o.OrderStatusId == 14 || o.OrderStatusId == 3)
                                        .Select(o => o.OrderAt.Year)
                                        .Distinct()
                                        .OrderBy(year => year)
                                        .ToListAsync();
        }
    }
}
