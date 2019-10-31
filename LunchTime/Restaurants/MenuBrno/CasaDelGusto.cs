using GeoCoordinatePortable;

namespace LunchTime.Restaurants.MenuBrno
{
	public class CasaDelGusto : ABrnoRestaurant
	{
		public override string Name => "Casa del gusto";

		public override string Url => "https://menubrno.cz/restaurace/0265-casa-del-gusto/";

		public override GeoCoordinate Location => new GeoCoordinate(49.200069, 16.613633);

		protected override uint[] SoupLinesPositions => new uint[] { };

		protected override uint FirstMealLinesPositions => 1;
	}
}
