using UnityEngine;

namespace TETORIEVER
{
    public abstract class CellObject : MonoBehaviour
    {
        public abstract void SetCellType(Cell.CellType cellType, bool selected);
    }
}