using System.Collections.Generic;

namespace TETORIEVER
{
    public static class InputConstants
    {
        public const string Place = "Place";
        public const string Rotate = "Rotate";
        public const string Hand1 = "Hand1";
        public const string Hand2 = "Hand2";
        public const string Hand3 = "Hand3";
        public const string Hand4 = "Hand4";

        private static readonly string[] _ButtonNames = new string[] 
        {
            Place,
            Rotate,
            Hand1,
            Hand2,
            Hand3,
            Hand4,
        };

        public static IReadOnlyCollection<string> ButtonNames => _ButtonNames;
    }
}