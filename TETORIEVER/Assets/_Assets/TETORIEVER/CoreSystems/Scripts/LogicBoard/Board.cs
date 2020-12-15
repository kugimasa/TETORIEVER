using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace TETORIEVER
{
    public class Board
    {
        private Cell[,] m_cells = default;

        private static IReadOnlyList<Vector2Int> Directions => _directions;

        private static readonly List<Vector2Int> _directions = new List<Vector2Int>()
        {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(1, 1),
            new Vector2Int(-1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, -1),
        };

        public Board(Vector2Int size)
        {
            // 最初は何もない盤面.
            m_cells = new Cell[size.x, size.y];
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    m_cells[x, y] = new Cell(new Vector2Int(x, y), Cell.CellType.None);
                }
            }
        }

        public bool CanPlace(IEnumerable<Cell> puts)
        {
            return puts.All(cell => IsEmpty(cell.m_position));
        }

        public void Place(IEnumerable<Cell> puts, out List<Vector2Int> removePositions)
        {
            // 置く処理.
            foreach (var cell in puts)
            {
                m_cells[cell.m_position.x, cell.m_position.y] = cell;
            }

            // 消すものを検索する処理.
            List<Cell> removes = new List<Cell>();
            foreach (var put in puts.Where(x => x.m_cellType == Cell.CellType.Vanish))
            {
                foreach (var dir in Directions)
                {
                    Stack<Cell> lines = new Stack<Cell>();
                    lines.Push(put);

                    while (GetCellType(lines.Peek().m_position + dir, out Cell cell) == Cell.CellType.Normal)
                    {
                        lines.Push(cell);
                    }

                    if (lines.Count > 1 && GetCellType(lines.Peek().m_position + dir, out Cell last) == Cell.CellType.Vanish)
                    {
                        lines.Push(last);
                        removes.AddRange(lines);
                    }
                }
            }

            // 消す処理.
            removePositions = new List<Vector2Int>();
            foreach (var remove in removes.Distinct())
            {
                removePositions.Add(remove.m_position);
                m_cells[remove.m_position.x, remove.m_position.y].m_cellType = Cell.CellType.None;
            }
        }

        private bool IsEmpty(Vector2Int pos)
        {
            if (!IsInRange(pos)) return false;
            return m_cells[pos.x, pos.y].IsEmpty;
        }

        private Cell.CellType GetCellType(Vector2Int pos, out Cell cell)
        {
            cell = default;
            if (!IsInRange(pos)) return Cell.CellType.Wall;
            cell = m_cells[pos.x, pos.y];
            return cell.m_cellType;
        }

        private bool IsInRange(Vector2Int pos)
        {
            if (pos.x < 0 || pos.x >= m_cells.GetLength(0)) return false;
            if (pos.y < 0 || pos.y >= m_cells.GetLength(1)) return false;
            return true;
        }
    }
}