using System.Text.Json;

namespace OSS_Main.Utils
{
	public static class SessionCommon
	{
		public static void SetObjectAsSession(this ISession session, string key, object value)
		{
			var jsonData = JsonSerializer.Serialize(value);
			session.SetString(key, jsonData);
		}
		public static T? GetSessionAsObject<T>(this ISession session, string key)
		{
			try
			{
				var value = session.GetString(key);
				return value == null ? default : JsonSerializer.Deserialize<T>(value);
			}
			catch (Exception)
			{
				return default;
			}
		}

		public static void RemoveSession(this ISession session, string key)
		{
			session.Remove(key);
		}
	}
}
