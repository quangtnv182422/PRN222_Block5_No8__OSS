using OSS_Main.Models.Entity;

namespace OSS_Main.Synchronize
{

    //Đây là giải pháp tạm thời vì bên GHN không cho cấu hình webhook khi đang có đường dẫn là localhost
    //Giải pháp này có thể chậm khi có quá nhiểu order đang cần cập nhật và phải gọi liên tục để cập nhật
    public class ShippingSync : BackgroundService
    {
        private readonly ILogger<ShippingSync> _logger;

        public ShippingSync(ILogger<ShippingSync> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
