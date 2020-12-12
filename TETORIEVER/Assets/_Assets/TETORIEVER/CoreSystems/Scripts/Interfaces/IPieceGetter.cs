using System.Collections.Generic;
using UnityEngine;

namespace TETORIEVER
{
    public interface IPieceGetter
    {
        /// <summary>
        /// 現在のCellを取得.
        /// 座標値は回転中心が(0, 0).
        /// </summary>
        IEnumerable<Cell> GetPiece(int index);

        /// <summary>
        /// 回転させる.
        /// GetPieceの値が変わる.
        /// </summary>
        void Rotate();
    }

    // ●◯の実装例.
    public class DefaultPieceGetter : IPieceGetter
    {
        private Cell[] m_cells = new Cell[]
        {
            new Cell(new Vector2Int(0, 0), Cell.CellType.Normal),
            new Cell(new Vector2Int(1, 0), Cell.CellType.Vanish),
        };

        public IEnumerable<Cell> GetPiece(int index)
        {
            // 固定.
            return m_cells;
        }

        public void Rotate()
        {
            for(int i = 0; i < m_cells.Length; i++)
            {
                var pos = m_cells[i].m_position;
                m_cells[i].m_position = new Vector2Int(pos.y, -pos.x);
            }
        }
    }
}