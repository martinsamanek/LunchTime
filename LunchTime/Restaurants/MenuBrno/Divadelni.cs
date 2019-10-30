using GeoCoordinatePortable;

namespace LunchTime.Restaurants.MenuBrno
{
	public class DivadelniMenu : ABrnoRestaurant
	{
		public override string Name => "Starobrněnská Pivnice Na Divadelní";

		public override string Url => "https://menubrno.cz/restaurace/0257-starobrnenska-pivnice-na-divadelni/";

		public override string Web => "https://www.nadivadelni.cz/denni-menu";

		public override GeoCoordinate Location => new GeoCoordinate(49.1948128, 16.6138478);
	}
}
