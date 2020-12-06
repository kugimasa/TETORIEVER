using UnityEngine;

namespace TETORIEVER
{
    public class Cell
    {
        public enum CellType
        {
            Wall = -1,
            None,
            Normal,
            Vanish,
        }

        public Vector2Int m_position = default;
        public CellType m_cellType = default;

        public Cell(Vector2Int position, CellType cellType)
        {
            m_position = position;
            m_cellType = cellType;
        }

        public bool IsEmpty => m_cellType == CellType.None;
    }
}