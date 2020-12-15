using UnityEngine;

namespace TETORIEVER
{
    public interface IPieceSource
    {
        Cell[][] PieceDatas { get; }
    }


    public class SimplePieceSource : IPieceSource
    {
        private Cell[][] m_cells = new Cell[][] {
            new Cell[]
        {
            new Cell(new Vector2Int(-1, 0), Cell.CellType.Normal),
            new Cell(new Vector2Int(0, 0), Cell.CellType.Vanish),
            new Cell(new Vector2Int(1, 0), Cell.CellType.Normal)
        },
            new Cell[]
        {
            new Cell(new Vector2Int(0, -1), Cell.CellType.Normal),
            new Cell(new Vector2Int(0, 0), Cell.CellType.Normal),
            new Cell(new Vector2Int(0, 1), Cell.CellType.Normal)
        },
            new Cell[]
        {
            new Cell(new Vector2Int(-1, -1), Cell.CellType.Normal),
            new Cell(new Vector2Int(-1, 0), Cell.CellType.Vanish),
            new Cell(new Vector2Int(1, 0), Cell.CellType.Normal),
            new Cell(new Vector2Int(0, 0), Cell.CellType.Normal)
        }
        };

        public Cell[][] PieceDatas { get { return m_cells; } }
    }
}