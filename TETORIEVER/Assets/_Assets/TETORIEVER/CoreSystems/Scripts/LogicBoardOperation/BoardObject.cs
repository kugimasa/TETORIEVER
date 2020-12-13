using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using System.Collections;

namespace TETORIEVER
{
    public class BoardObject : MonoBehaviour, IBoard
    {
        [SerializeField] private Vector2Int m_size = new Vector2Int(8, 8);
        [SerializeField] private GameEventInt m_onCellDeleted = default;

        [Header("Generator")]
        [SerializeField] private Grid m_gridPrefab = default;
        [SerializeField] private Vector3 m_origin = Vector3.zero;
        [SerializeField] private Vector3 m_offsetX = Vector3.right;
        [SerializeField] private Vector3 m_offsetY = Vector3.forward;

        [SerializeField] private List<Grid> m_grids = new List<Grid>();

        private Board m_board = default;

        public bool IsBusy { get; private set; } = false;

        public bool CanPlace(IEnumerable<Cell> puts)
        {
            return m_board.CanPlace(puts);
        }

        public void Place(IEnumerable<Cell> puts)
        {
            StartCoroutine(PlaceCoroutine(puts));
        }

        private void Awake()
        {
            m_board = new Board(m_size);
            Services.Board = this;
        }

        private IEnumerator PlaceCoroutine(IEnumerable<Cell> puts)
        {
            IsBusy = true;
            m_board.Place(puts, out List<Vector2Int> positions);
            yield return Services.BoardEffect?.PlaceCoroutine(puts);
            m_onCellDeleted?.Publish(positions.Count);
            yield return Services.BoardEffect?.RemoveCoroutine(positions);
            IsBusy = false;
        }
        
        [ContextMenu("GenerateItem")]
        private void GenerateGrids()
        {
            // 既にあるものは削除.
            foreach(var grid in m_grids)
            {
                if (grid?.gameObject == null) continue;
                DestroyImmediate(grid.gameObject);
            }
            m_grids.Clear();

            // 生成.
            for(int x = 0; x < m_size.x; x++)
            {
                for(int y = 0; y < m_size.y; y++)
                {
                    var grid = Instantiate(m_gridPrefab, m_origin + m_offsetX * x + m_offsetY * y, Quaternion.identity, transform);
                    grid.m_position = new Vector2Int(x, y);
                    m_grids.Add(grid);
                }
            }
        }
    }
}