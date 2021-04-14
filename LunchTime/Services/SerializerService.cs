using Newtonsoft.Json;

namespace LunchTime.Services
{
    public class SerializerService
    {
		public static string SerializeToJson<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		public static T DeserializeFromJson<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
		}
	}
}
