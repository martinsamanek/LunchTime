namespace LunchTime.Restaurants.MenuBrno
{
    public class Statl : MenuBrnoBase
    {
        public override string Name => "Štatl";
        public override string Url => "https://menubrno.cz/restaurace/0349-Statl/";
        public override string Web => "";
        protected override int[] SoupLinesPositions => new[] {1};
        protected override int FirstMealLinesPositions => 2;
    }
}