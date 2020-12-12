using UnityEngine;

namespace TETORIEVER
{
    public struct Cell
    {
        public enum CellType
        {
            Wall = -1,
            None,
            Normal,
            Vanish,
        }

        public Vector2Int m_position;
        public CellType m_cellType;

        public Cell(Vector2Int position, CellType cellType)
        {
            m_position = position;
            m_cellType = cellType;
        }

        public bool IsEmpty => m_cellType == CellType.None;
    }
}