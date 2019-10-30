using GeoCoordinatePortable;

namespace LunchTime.Restaurants.MenuBrno
{
	public class Baroko : ABrnoRestaurant
	{
		public override string Name => "Baroko";

		public override string Url => "https://menubrno.cz/restaurace/0076-restaurace-baroko/";

		public override GeoCoordinate Location => new GeoCoordinate(49.1938094, 16.6116847);
	}
}
