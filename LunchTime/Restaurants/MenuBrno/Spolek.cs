namespace LunchTime.Restaurants.MenuBrno
{
    public class Spolek : MenuBrnoBase
    {
        public override string Name => "Spolek";
        public override string Url => "https://menubrno.cz/restaurace/0169-spolek/";
        public override string Web => "";
        protected override int[] SoupLinesPositions => new[] {1};
        protected override int FirstMealLinesPositions => 2;
    }
}