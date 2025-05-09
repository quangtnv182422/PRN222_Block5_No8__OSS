using System.Security.Cryptography;
using System.Text;

namespace OSS_Main.Utils
{
	public class UtilHelper
	{
		public static async Task<byte[]> DownloadFileAsBytesAsync(string url)
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					return await httpClient.GetByteArrayAsync(url);
				}
			}
			catch (Exception)
			{
				return Array.Empty<byte>();
			}
		}
		public static byte[] GetBytesFromIFormFile(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return Array.Empty<byte>();

			using (var memoryStream = new MemoryStream())
			{
				file.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}

		public static string ComputeSha256Hash(byte[] data)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] hashBytes = sha256.ComputeHash(data);
				var builder = new StringBuilder();
				foreach (var b in hashBytes)
				{
					builder.Append(b.ToString("x2"));
				}
				return builder.ToString();
			}
		}
	}
}
