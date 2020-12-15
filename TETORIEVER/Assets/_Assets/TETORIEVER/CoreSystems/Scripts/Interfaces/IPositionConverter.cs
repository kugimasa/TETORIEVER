using UnityEngine;

namespace TETORIEVER
{
    public interface IPositionConverter
    {
        /// <summary>ボードの座標をワールド座標系に変換.</summary>
        /// <returns>座標が不適切な時はnull</returns>
        Vector3? BoardToWorldPosition(Vector2Int position);
    }
}