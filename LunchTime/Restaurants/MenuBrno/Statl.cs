using GeoCoordinatePortable;

namespace LunchTime.Restaurants.MenuBrno
{
	public class Statl : ABrnoRestaurant
	{
		public override string Name => "Štatl";

		public override string Url => "https://menubrno.cz/restaurace/0349-Statl/";

		public override GeoCoordinate Location => new GeoCoordinate(49.1955489, 16.6074561);
	}
}
