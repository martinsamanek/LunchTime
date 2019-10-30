using GeoCoordinatePortable;

namespace LunchTime.Restaurants.MenuBrno
{
	public class Spolek : ABrnoRestaurant
	{
		public override string Name => "Spolek";

		public override string Url => "https://menubrno.cz/restaurace/0169-spolek/";

		public override GeoCoordinate Location => new GeoCoordinate(49.1939506, 16.6125336);
	}
}
