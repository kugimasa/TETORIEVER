using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER
{
    public class PieceObjects : MonoBehaviour
    {
        [SerializeField] private int m_handIndex = 0;
        [SerializeField] private Vector2Int m_boardCenter = new Vector2Int(3, 3);

        [Header("Prefabs")]
        [SerializeField] private CellObject m_cellPrefab = default;

        [Header("Variables")]
        [SerializeField] private ReadonlyVector3 m_cursorWorldPosition = default;
        [SerializeField] private GameEventListener m_onPieceRotated = default;
        [SerializeField] private GameEventListener m_onPiecePlaced = default;
        [SerializeField] private GameEventStringListener m_onPieceChanged = default;
        [SerializeField] private GameEventBoolListener m_onIsPlayingChanged = default;

        private string[] m_hands = new string[]
        {
            InputConstants.Hand1,
            InputConstants.Hand2,
            InputConstants.Hand3,
            InputConstants.Hand4,
        };

        private bool m_isSelected = false;
        private Transform m_transform = default;
        private Vector3 m_centerPosition = default;
        private List<CellObject> m_cellObjects = new List<CellObject>();

        private void Start()
        {
            m_transform = transform;
            m_onIsPlayingChanged?.Subscribe(OnInPlayingChanged)?.DisposeOnSceneUnLoaded();
            m_onPiecePlaced?.Subscribe(UpdatePieceObjects)?.DisposeOnSceneUnLoaded();
            m_onPieceRotated?.Subscribe(UpdatePieceObjects)?.DisposeOnSceneUnLoaded();
            m_onPieceChanged?.Subscribe(SetSelects)?.DisposeOnSceneUnLoaded();
            
            if (!CheckPosition(m_boardCenter, out Vector3 centerPosition)) return;
            m_centerPosition = centerPosition;
            UpdatePieceObjects();
        }

        private void Update()
        {
            m_transform.position = m_cursorWorldPosition;
        }

        private void OnInPlayingChanged(bool playing)
        {
            if (playing)
            {
                gameObject?.SetActive(true);
                SetSelects(m_hands[0]);
            }
            else
            {
                gameObject?.SetActive(false);
            }
        }

        private void SetSelects(string hand)
        {
            m_isSelected = false;
            for(int i = 0; i < m_hands.Length; i++)
            {
                if (hand != m_hands[i]) continue;
                if (i != m_handIndex) continue;
                m_isSelected = true;
                break;
            }
            UpdatePieceObjects();
        }

        private void UpdatePieceObjects()
        {
            int index = 0;
            m_cellObjects.ForEach(x => x.gameObject.SetActive(false));
            foreach (var cell in Services.PieceGetter.GetPiece(m_handIndex))
            {
                if (!CheckPosition(m_boardCenter + cell.m_position, out Vector3 pos)) continue;

                CellObject obj = null;
                if (index < m_cellObjects.Count)
                {
                    obj = m_cellObjects[index];
                    obj.gameObject.SetActive(true);
                }
                else
                {
                    obj = Instantiate(m_cellPrefab, m_transform);
                    m_cellObjects.Add(obj);
                }
                obj.SetCellType(cell.m_cellType, m_isSelected);
                obj.transform.localPosition = pos - m_centerPosition;

                index++;
            }
        }

        private bool CheckPosition(Vector2Int boardPosition, out Vector3 worldPosition)
        {
            var pos = Services.PositionConverter.BoardToWorldPosition(boardPosition);
            if (!pos.HasValue)
            {
#if UNITY_EDITOR
                Debug.LogError("Out of range");
#endif
                worldPosition = default;
                return false;
            }

            worldPosition = pos.Value;
            return true;
        }
    }
}