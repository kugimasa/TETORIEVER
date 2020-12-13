using System.Linq;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private ReadonlyBool m_isPlaying = default;
        [SerializeField] private ReadonlyVector2Int m_cursorPosition = default;

        [Header("Events")]
        [SerializeField] private GameEventStringListener m_onInputRecieved = default;
        [SerializeField] private GameEventString m_onPieceChanged = default;
        [SerializeField] private GameEvent m_onPiecePlaced = default;
        [SerializeField] private GameEvent m_onPieceRotated = default;

        private string[] m_handButtons = new string[]
        {
            InputConstants.Hand1,
            InputConstants.Hand2,
            InputConstants.Hand3,
            InputConstants.Hand4,
        };

        private int m_handIndex = 0;

        private void Start()
        {
            m_onInputRecieved?.Subscribe(OnInputRecieved).DisposeOnSceneUnLoaded();
        }

        private void OnInputRecieved(string buttonName)
        {
            if (!m_isPlaying) return;
            if (Services.Board.IsBusy) return;

            switch (buttonName)
            {
                case InputConstants.Place:
                    // CanPlaceとPlaceで2回走るのでToArrayで即時評価.
                    var puts = Services.PieceGetter.GetPiece(m_handIndex).Select(x => new Cell(x.m_position + m_cursorPosition, x.m_cellType)).ToArray();
                    if (Services.Board.CanPlace(puts))
                    {
                        Services.Board.Place(puts);
                        m_onPiecePlaced?.Publish();
                    }
                    break;
                case InputConstants.Rotate:
                    Services.PieceGetter.Rotate();
                    m_onPieceRotated?.Publish();
                    break;
                default:
                    int index = -1;
                    for (int i = 0; i < m_handButtons.Length; i++)
                    {
                        if (buttonName != m_handButtons[i]) continue;
                        index = i;
                    }

                    if(index != m_handIndex)
                    {
                        m_handIndex = index;
                        m_onPieceChanged?.Publish(buttonName);
                    }
                    break;
            }
        }
    }
}