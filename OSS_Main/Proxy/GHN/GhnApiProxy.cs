using OSS_Main.Models.DTO.GHN;
using System.Text;
using System.Text.Json;

namespace OSS_Main.Proxy.GHN
{
    public class GhnApiProxy : IGhnProxy
    {
        private readonly HttpClient _httpClient;
        private readonly GHNSettings _ghnSettings;

        public GhnApiProxy(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _ghnSettings = configuration.GetSection("GHNSettings").Get<GHNSettings>();
            _httpClient.DefaultRequestHeaders.Add("Token", _ghnSettings.Token);
        }

        public async Task<string> GetProvincesAsync()
        {
            var response = await _httpClient.GetAsync($"{_ghnSettings.BaseUrl}{_ghnSettings.Endpoints.Provinces}");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetDistrictsAsync(int provinceId)
        {
            var jsonBody = JsonSerializer.Serialize(new { province_id = provinceId });
            var response = await _httpClient.PostAsync($"{_ghnSettings.BaseUrl}{_ghnSettings.Endpoints.Districts}",
                new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetWardsAsync(int districtId)
        {
            var jsonBody = JsonSerializer.Serialize(new { district_id = districtId });
            var response = await _httpClient.PostAsync($"{_ghnSettings.BaseUrl}{_ghnSettings.Endpoints.Wards}",
                new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAvailableServicesAsync(int shopId, int fromDistrictId, int toDistrictId)
        {
            var jsonBody = JsonSerializer.Serialize(new
            {
                shop_id = shopId,
                from_district = fromDistrictId,
                to_district = toDistrictId
            });
            var response = await _httpClient.PostAsync($"{_ghnSettings.BaseUrl}{_ghnSettings.Endpoints.AvailableServices}",
                new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CalculateShippingFeeAsync(int shopId, int fromDistrictId, int toDistrictId, int weight, int length, int width, int height, int serviceId, int serviceTypeId)
        {
            var jsonBody = JsonSerializer.Serialize(new
            {
                shop_id = shopId,
                from_district_id = fromDistrictId,
                to_district_id = toDistrictId,
                weight,
                length,
                width,
                height,
                service_id = serviceId,
                service_type_id = serviceTypeId
            });
            var response = await _httpClient.PostAsync($"{_ghnSettings.BaseUrl}{_ghnSettings.Endpoints.ShippingFee}",
                new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SendShippingOrderAsync(ShippingOrder order)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var json = JsonSerializer.Serialize(order, options);

            var response = await _httpClient.PostAsync($"{_ghnSettings.BaseUrl}{_ghnSettings.Endpoints.CreateOrder}",
                new StringContent(json, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetTokenPrintOrder(OrderRequest order)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var json = JsonSerializer.Serialize(order, options);

            var response = await _httpClient.PostAsync($"{_ghnSettings.BaseUrl}{_ghnSettings.Endpoints.TokenPrint}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PrintOrderAsync(OrderRequest order)
        {
            var tokenResponse = await GetTokenPrintOrder(order);
            using (JsonDocument doc = JsonDocument.Parse(tokenResponse))
            {
                var token = doc.RootElement.GetProperty("data").GetProperty("token").GetString();

                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Failed to get print token.");
                }

                var printUrl = $"https://dev-online-gateway.ghn.vn/a5/public-api/printA5?token={token}";

                var response = await _httpClient.GetAsync(printUrl);

                return await response.Content.ReadAsStringAsync();

            }
        }

        public async Task<bool> CancelOrderOnGhnAsync(string orderCode)
        {
            var jsonBody = JsonSerializer.Serialize(new { order_codes = new List<string> { orderCode } });

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_ghnSettings.BaseUrl}/v2/switch-status/cancel")
            {
                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
            };

            request.Headers.Add("Token", _ghnSettings.Token);
            request.Headers.Add("ShopId", _ghnSettings.ShopId);

            var response = await _httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return true; 
            }
            else
            {
                Console.WriteLine($"GHN Cancel Order Error: {responseBody}");
                return false; 
            }
        }
    }
}
