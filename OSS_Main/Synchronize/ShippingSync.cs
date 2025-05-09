﻿using Microsoft.AspNetCore.SignalR;
using OSS_Main.Hubs;
using OSS_Main.Models.DTO.GHN;
using OSS_Main.Proxy.GHN;
using OSS_Main.Service.Interface;

namespace OSS_Main.Synchronize
{
    public class ShippingSync : BackgroundService
    {
        private readonly ILogger<ShippingSync> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public ShippingSync(ILogger<ShippingSync> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                // Gọi xử lý logic cập nhật GHN
                await SyncOrdersWithGHN();

                await Task.Delay(1000, stoppingToken);
            }
        }

        // Hàm riêng để xử lý GHN, được gọi từ ExecuteAsync
        private async Task SyncOrdersWithGHN()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                var ghnService = scope.ServiceProvider.GetRequiredService<IGhnProxy>();
                var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<ShippingSyncHub>>();


                var listOrderDetails = await orderService.GetAllOrderShippingAsync();

                foreach (var orderD in listOrderDetails)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(orderD.OrderCode_GHN))
                        {
                            _logger.LogWarning("Empty order code for order ID: {id}", orderD.OrderId);
                            continue;
                        }

                        var orderRequest = new OrderRequest { order_code = orderD.OrderCode_GHN };
                        var orderDetails = await ghnService.GetOrderDetails(orderRequest);

                        if (orderDetails != null)
                        {
                            if (!orderDetails.status.Equals(orderD.OrderStatus.OrderStatusName))
                            {
                                var currentStatus = await orderService.GetOrderStatusByNameAsync(orderDetails.status);
                                orderD.OrderStatusId = currentStatus.OrderStatusId;// update lại status như với bên GHN để đồng bộ
                                await orderService.UpdateOrderOnGHNAsync(orderD);

                                //nếu status là cancel thì phải trả lại số lượng vào kho
                                if (orderDetails.status.Equals("cancel"))
                                {
                                    await productService.UpdateProductQuantityAfterCancel(orderD.OrderItemOrders);//Trả lại số lượng
                                }

                                //đoạn này dùng signalR để cập nhật realtime
                                await hubContext.Clients.All.SendAsync("UpdateStatus");

                                //nhớ làm thêm luồng khách bom hàng và shipper trả về
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error while syncing order with code: {code}", orderD.OrderCode_GHN);
                    }
                }
            }
        }

    }
}
