using System.Collections;

namespace TETORIEVER
{
    public interface IStartEffect
    {
        /// <summary>ゲーム開始前の演出用コルーチン</summary>
        IEnumerator Play();
    }
}