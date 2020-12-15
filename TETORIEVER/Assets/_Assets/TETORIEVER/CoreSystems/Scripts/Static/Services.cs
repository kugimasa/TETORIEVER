namespace TETORIEVER
{
    public static class Services
    {
        public static IBoard Board { get; set; } = new DefaultBoard();
        public static IBoardEffect BoardEffect { get; set; }
        public static IPositionConverter PositionConverter { get; set; }
        public static IPieceGetter PieceGetter { get; set; } = new DefaultPieceGetter();
        public static IStartEffect StartEffect { get; set; }
        public static IResultView ResultView { get; set; }
    }
}