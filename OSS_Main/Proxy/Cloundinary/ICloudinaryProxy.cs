namespace OSS_Main.Proxy.Cloundinary
{
    public interface ICloudinaryProxy
    {
        Task<string> UploadImageAsync(IFormFile file);
        void DeleteImage(string imageUrl);
    }
}
