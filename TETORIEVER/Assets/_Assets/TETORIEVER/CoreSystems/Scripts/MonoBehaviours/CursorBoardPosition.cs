using SilCilSystem.Variables;
using System.Linq;
using UnityEngine;

namespace TETORIEVER
{
    public class CursorBoardPosition : MonoBehaviour, IPositionConverter
    {
        private const float MaxDistance = 100f;

        [SerializeField] private VariableVector2Int m_cursorBoardPosition = default;
        [SerializeField] private VariableVector3 m_boardWorldPosition = default;
        [SerializeField] private LayerMask m_gridLayer = default;

        [Header("Debug")]
        [SerializeField] private Transform m_followObject = default;

        private Camera m_camera = default;
        private Grid[] m_grids = default;

        private void Awake()
        {
            m_camera = Camera.main;
            // 処理的に重いかもだけど、独立して動くようにするためにやむなく.
            m_grids = FindObjectsOfType<Grid>();
            Services.PositionConverter = this;
        }

        private void Update()
        {
            UpdateCursorPosition();
            UpdateBoardPosition();
        }

        private void UpdateCursorPosition()
        {
            if (m_cursorBoardPosition == null) return;

            var pos = Input.mousePosition;
            var ray = m_camera.ScreenPointToRay(pos);

            var boardPosition = new Vector2Int(-1, -1);
            do
            {
                if (!Physics.Raycast(ray, out RaycastHit hit, MaxDistance, m_gridLayer.value)) return;
                if (!hit.collider.TryGetComponent(out Grid grid)) return;
                boardPosition = grid.m_position;
            } while (false);

            m_cursorBoardPosition.Value = boardPosition;
        }

        private void UpdateBoardPosition()
        {
            if (m_cursorBoardPosition == null) return;
            if (m_boardWorldPosition == null) return;

            var grid = m_grids?.FirstOrDefault(x => x.m_position == m_cursorBoardPosition);
            m_boardWorldPosition.Value = (grid == null) ? -Vector3.one : grid.transform.position;

            // Debug用.
            if (m_followObject == null) return;
            m_followObject.transform.position = m_boardWorldPosition;
        }

        public Vector3? BoardToWorldPosition(Vector2Int position)
        {
            foreach(var grid in m_grids)
            {
                if (grid.m_position != position) continue;
                return grid.transform.position;
            }
            return null;
        }
    }
}