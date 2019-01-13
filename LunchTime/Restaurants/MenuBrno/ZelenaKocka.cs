namespace LunchTime.Restaurants.MenuBrno
{
    public class ZelenaKocka : MenuBrnoBase
    {
        public override string Name => "Zelená Kočka";
        public override string Url => "https://menubrno.cz/restaurace/0262-zelena-kocka---pivovarsky-restaurant/";
        public override string Web => "";
        protected override int[] SoupLinesPositions => new[] {1};
        protected override int FirstMealLinesPositions => 2;
    }
}