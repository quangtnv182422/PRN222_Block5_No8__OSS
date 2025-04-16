namespace OSS_Main.Models.DTO.GHN
{
    public class GHNSettings
    {
        public string Token { get; set; }
        public string BaseUrl { get; set; }
        public string TestBaseUrl { get; set; }
        public string ShopId { get; set; }
        public GHNEndpoints Endpoints { get; set; }
    }
}
