using GeoCoordinatePortable;

namespace LunchTime.Restaurants.MenuBrno
{
	public class Jakoby : ABrnoRestaurant
	{
		public override string Name => "Jakoby";

		public override string Url => "https://menubrno.cz/restaurace/0091-jakoby/";

		public override GeoCoordinate Location => new GeoCoordinate(49.1970253, 16.6086578);

		protected override uint[] SoupLinesPositions => new[] { 2U };

		protected override uint FirstMealLinesPositions => 4;
	}
}
