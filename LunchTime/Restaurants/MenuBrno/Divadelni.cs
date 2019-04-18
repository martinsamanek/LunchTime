namespace LunchTime.Restaurants.MenuBrno
{
    public class DivadelniMenu : MenuBrnoBase
    {
        public override int Id => 4;
        public override string Name => "Starobrněnská Pivnice Na Divadelní";
        public override string Url => "https://menubrno.cz/restaurace/0257-starobrnenska-pivnice-na-divadelni/";
        public override string Web => "https://www.nadivadelni.cz/denni-menu";
        protected override int[] SoupLinesPositions => new[] { 1 };
        protected override int FirstMealLinesPositions => 2;
    }
}