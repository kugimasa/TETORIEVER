using System.Collections.Generic;

namespace TETORIEVER
{
    public interface IBoard
    {
        bool CanPlace(IEnumerable<Cell> puts);
        void Place(IEnumerable<Cell> puts);
        /// <summary>処理中はtrue, 終わったらfalseにすること</summary>
        bool IsBusy { get; }
    }

    // 何もしない.
    public class DefaultBoard : IBoard
    {
        public bool CanPlace(IEnumerable<Cell> puts) => true;
        public bool IsBusy => false;
        public void Place(IEnumerable<Cell> puts) { }
    }
}