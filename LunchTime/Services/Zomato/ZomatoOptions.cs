namespace LunchTime.Services.Zomato
{
    public class ZomatoOptions
    {
        public static string SettingsKey => "Zomato";
        public string ApiBaseAddress { get; set; } = "https://developers.zomato.com";
        public string ApiKey { get; set; }
    }
}