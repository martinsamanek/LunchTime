using System;
using LunchTime.Restaurants.TODO;

namespace LunchTime.Zomato
{
    public class ZomatoClientAccessor
    {
        private static IZomatoClient _instance;

        public static void Init(IZomatoClient client)
        {
            _instance = client;
            IsInitialized = true;
        }

        public static bool IsInitialized { get; private set; } = false;

        public static IZomatoClient Instance => IsInitialized ? _instance : throw new InvalidOperationException($"{nameof(ZomatoClientAccessor)} has not been initialized. Call {nameof(Init)} method first");
    }
}