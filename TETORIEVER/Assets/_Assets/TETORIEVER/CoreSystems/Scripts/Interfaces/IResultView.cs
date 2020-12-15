using System.Collections;

namespace TETORIEVER
{
    public interface IResultView
    {
        /// <summary>リザルトを表示するコルーチン</summary>
        IEnumerator Show();
    }
}