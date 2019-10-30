using GeoCoordinatePortable;

namespace LunchTime.Restaurants.MenuBrno
{
	public class ZelenaKocka : ABrnoRestaurant
	{
		public override string Name => "Zelená Kočka";

		public override string Url => "https://menubrno.cz/restaurace/0262-zelena-kocka---pivovarsky-restaurant/";

		public override GeoCoordinate Location => new GeoCoordinate(49.1969615, 16.6059031);
	}
}
