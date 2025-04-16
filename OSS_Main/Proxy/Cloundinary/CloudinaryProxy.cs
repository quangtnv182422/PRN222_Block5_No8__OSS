using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace OSS_Main.Proxy.Cloundinary
{
    public class CloudinaryProxy : ICloudinaryProxy
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryProxy(IConfiguration configuration)
        {
            var cloudinarySettings = configuration.GetSection("CloudinarySettings");
            string cloudName = cloudinarySettings["CloudName"];
            string apiKey = cloudinarySettings["ApiKey"];
            string apiSecret = cloudinarySettings["ApiSecret"];

            if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                throw new Exception("Cloudinary configuration is missing! Check appsettings.json.");
            }

            Console.WriteLine($"Cloudinary Config Loaded: CloudName={cloudName}, ApiKey={apiKey}");

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Width(500).Height(500).Crop("limit")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult?.SecureUrl.AbsoluteUri;
        }

        public void DeleteImage(string imageUrl)
        {
            var publicId = GetPublicIdFromUrl(imageUrl);
            if (string.IsNullOrEmpty(publicId))
            {
                throw new Exception("Public ID is missing or invalid.");
            }

            var deleteParams = new DeletionParams(publicId);
            var deletionResult = _cloudinary.Destroy(deleteParams);

            if (deletionResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Failed to delete image from Cloudinary");
            }
        }

        private string GetPublicIdFromUrl(string imageUrl)
        {
            var uri = new Uri(imageUrl);
            var segments = uri.AbsolutePath.Split('/');
            return segments.Length > 0 ? segments[segments.Length - 1].Split('.')[0] : null;
        }
    }
}
