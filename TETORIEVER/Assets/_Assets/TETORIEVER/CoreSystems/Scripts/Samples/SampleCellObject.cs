using UnityEngine;

namespace TETORIEVER
{
    public class SampleCellObject : CellObject
    {
        [SerializeField] private GameObject m_normal = default;
        [SerializeField] private GameObject m_vanish = default;
        [SerializeField] private GameObject m_box = default;
        [SerializeField] private bool m_activeWhenUnselected = false;

        public override void SetCellType(Cell.CellType cellType, bool selected)
        {
            if (m_activeWhenUnselected)
            {
                m_normal.SetActive(cellType == Cell.CellType.Normal);
                m_vanish.SetActive(cellType == Cell.CellType.Vanish);
            }
            else
            {
                m_normal.SetActive(selected && cellType == Cell.CellType.Normal);
                m_vanish.SetActive(selected && cellType == Cell.CellType.Vanish);
            }
            m_box.SetActive(selected);
        }
    }
}