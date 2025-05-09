namespace OSS_Main.Utils
{
	public class Mapper
	{
		public static TDestination? Map<TSource, TDestination>(TSource source) where TDestination : class, new()
			where TSource : class
		{
			if (source == null) return null;
			var destination = new TDestination();
			var sourceProperties = source.GetType().GetProperties();
			var destinationProperties = destination.GetType().GetProperties().ToDictionary(p => p.Name);
			foreach (var sourceProperty in sourceProperties)
			{
				if (!destinationProperties.TryGetValue(sourceProperty.Name, out var destinationProperty))
					continue;
				if (!destinationProperty.CanWrite)
					continue;
				var sourceValue = sourceProperty.GetValue(source);

				if (sourceValue == null)
				{
					destinationProperty.SetValue(destination, null);
					continue;
				}
				var sourceType = sourceProperty.PropertyType;
				var destinationType = destinationProperty.PropertyType;
				if (typeof(System.Collections.IEnumerable).IsAssignableFrom(sourceType) && sourceType != typeof(string))
				{
					var sourceElementType = sourceType.IsGenericType ? sourceType.GetGenericArguments()[0] : null;
					var destElementType = destinationType.IsGenericType ? destinationType.GetGenericArguments()[0] : null;

					if (sourceElementType != null && destElementType != null)
					{
						var listType = typeof(List<>).MakeGenericType(destElementType);
						var list = (System.Collections.IList)Activator.CreateInstance(listType)!;

						foreach (var item in (System.Collections.IEnumerable)sourceValue)
						{
							var mapMethod = typeof(Mapper).GetMethod("Map")!.MakeGenericMethod(sourceElementType, destElementType);
							var mappedItem = mapMethod.Invoke(null, new object[] { item });
							list.Add(mappedItem);
						}

						destinationProperty.SetValue(destination, list);
					}

					continue;
				}
				if (sourceType.IsClass && sourceType != typeof(string) && destinationType.IsClass && destinationType != typeof(string))
				{
					var method = typeof(Mapper).GetMethod("Map")!.MakeGenericMethod(sourceType, destinationType);
					var mappedValue = method.Invoke(null, new object[] { sourceValue });
					destinationProperty.SetValue(destination, mappedValue);
				}
				else
				{
					destinationProperty.SetValue(destination, sourceValue);
				}
			}
			return destination;
		}
	}
}
