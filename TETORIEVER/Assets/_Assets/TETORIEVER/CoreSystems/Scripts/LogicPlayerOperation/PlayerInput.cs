using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private ReadonlyBool m_enabled = default;
        [SerializeField] private GameEventString m_onInputRecieved = default;

        private void Update()
        {
            if (!m_enabled) return;

            foreach(var button in InputConstants.ButtonNames)
            {
                if (GetButtonDown(button))
                {
                    m_onInputRecieved?.Publish(button);
                    return;
                }
            }
        }

        public bool GetButtonDown(string buttonName)
        {
            switch (buttonName)
            {
                case InputConstants.Place:
                    return Input.GetKeyDown(KeyCode.Mouse0);
                case InputConstants.Hand1:
                    return Input.GetKeyDown(KeyCode.A);
                case InputConstants.Hand2:
                    return Input.GetKeyDown(KeyCode.S);
                case InputConstants.Hand3:
                    return Input.GetKeyDown(KeyCode.D);
                case InputConstants.Hand4:
                    return Input.GetKeyDown(KeyCode.F);
                case InputConstants.Rotate:
                    return Input.GetKeyDown(KeyCode.Mouse1);
                default:
                    return false;
            }
        }
    }
}