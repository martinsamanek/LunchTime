namespace LunchTime.Models
{
	public class Meal
	{
		public Meal(string name, string price)
		{
			Name = name;
			Price = price;
		}

		public string Name { get; }

		public string Price { get; }
	}
}
