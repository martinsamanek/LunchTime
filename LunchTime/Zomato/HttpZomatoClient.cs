using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using LunchTime.Restaurants.TODO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LunchTime.Zomato
{
    public class HttpZomatoClient : IZomatoClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<ZomatoOptions> _options;

        private const string DailyMenuRelativeAddress = "/api/v2.1/dailymenu";
        private const string UserKeyRequestHeaderName = "user_key";
        private const string RestaurandIdKey = "res_id";
        public HttpZomatoClient(IHttpClientFactory httpClientFactory, IOptionsMonitor<ZomatoOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options;
        }

        public async Task<ZomatoDailyMenu> GetMenuAsync(int restaurantId)
        {
            var client = _httpClientFactory.CreateClient();

            var builder = new UriBuilder(new Uri(new Uri(_options.CurrentValue.ApiBaseAddress), DailyMenuRelativeAddress));
            var query = HttpUtility.ParseQueryString(builder.Query);
            query[RestaurandIdKey] = restaurantId.ToString();
            builder.Query = query.ToString();
            
            client.DefaultRequestHeaders.Add(UserKeyRequestHeaderName, _options.CurrentValue.ApiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.GetAsync(builder.ToString());

            return !result.IsSuccessStatusCode
                ? null
                : JsonConvert.DeserializeObject<ZomatoDailyMenu>(await result.Content.ReadAsStringAsync());
        }
    }
}