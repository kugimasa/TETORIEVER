using UnityEngine;
using SilCilSystem.Variables;
using UnityEngine.UI;

namespace TETORIEVER
{
    public class HandsView : MonoBehaviour
    {
        [SerializeField] private GameEventStringListener m_onPieceChanged = default;
        [SerializeField] private Toggle[] m_toggles = default;

        private string[] m_names = new string[]
        {
            InputConstants.Hand1,
            InputConstants.Hand2,
            InputConstants.Hand3,
            InputConstants.Hand4,
        };

        private void Start()
        {
            m_toggles[0].isOn = true;
            m_onPieceChanged?.Subscribe(SelectToggle)?.DisposeOnSceneUnLoaded();
        }

        private void SelectToggle(string selected)
        {
            for(int i = 0; i < m_names.Length; i++)
            {
                if (selected != m_names[i]) continue;
                m_toggles[i].isOn = true;
            }
        }
    }
}