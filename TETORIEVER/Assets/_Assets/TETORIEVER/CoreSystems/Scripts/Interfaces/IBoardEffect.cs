using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace TETORIEVER
{
    public interface IBoardEffect
    {
        /// <summary>コマを置く演出用コルーチン</summary>
        IEnumerator PlaceCoroutine(IEnumerable<Cell> puts);
        /// <summary>コマを消す演出用コルーチン</summary>
        IEnumerator RemoveCoroutine(IEnumerable<Vector2Int> removes);
    }
}