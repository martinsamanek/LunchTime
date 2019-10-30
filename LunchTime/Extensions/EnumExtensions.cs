using System;

namespace LunchTime
{
	public static class EnumExtensions
	{
		public static T? ToEnum<T>(this string value, T? defaultValue = null) where T : struct
		{
			T? result = defaultValue;
			var typeAsString = typeof(T).ToString();

			if (string.IsNullOrEmpty(value) || !typeof(T).IsEnum)
				throw new InvalidOperationException($"Invalid Enum Type '{ typeAsString }' or input parameter '{ nameof(value) }' is empty: '{ value }'.");

			try
			{
				var success = Enum.TryParse<T>(value, true, out var parseResult);
				if (success)
					result = parseResult;

			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Invalid cast for {value} into type { typeAsString }", ex);
			}
			return result;
		}
	}
}
