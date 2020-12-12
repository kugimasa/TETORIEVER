using UnityEngine;

namespace TETORIEVER
{
    public class SampleCellObject : CellObject
    {
        [SerializeField] private GameObject m_normal = default;
        [SerializeField] private GameObject m_vanish = default;

        public override void SetCellType(Cell.CellType cellType, bool selected)
        {
            m_normal.SetActive(selected && cellType == Cell.CellType.Normal);
            m_vanish.SetActive(selected && cellType == Cell.CellType.Vanish);
        }
    }
}