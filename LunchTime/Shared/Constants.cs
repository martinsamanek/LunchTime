namespace LunchTime.Shared
{
    public static class Constants
    {
        /// <summary>
        /// Cookie name used for storing bookmarked restaurants
        /// </summary>
        public const string BookmarkedCookieName = "bookmarked";

        /// <summary>
        /// Cookie name used for storing selected city
        /// </summary>
        public const string SelectedCityCookieName = "selectedCity";

        /// <summary>
        /// SignalR method for vote notification
        /// </summary>
        public const string SignalRVoteNotification = "VoteNotification";
    }
}