namespace LunchTime.Zomato
{
    public interface IZomatoClient
    {
        ZomatoDailyMenu GetMenu(int restaurantId);
    }
}