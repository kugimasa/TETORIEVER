using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER
{
    public class BoardObject : MonoBehaviour
    {
        [SerializeField] private Vector2Int m_size = new Vector2Int(8, 8);

        [Header("Generator")]
        [SerializeField] private Grid m_gridPrefab = default;
        [SerializeField] private Vector3 m_origin = Vector3.zero;
        [SerializeField] private Vector3 m_offsetX = Vector3.right;
        [SerializeField] private Vector3 m_offsetY = Vector3.forward;

        [SerializeField] private List<Grid> m_grids = new List<Grid>();

        private Board m_board = default;

        private void Start()
        {
            m_board = new Board(m_size);

            m_board.Place(new Cell[] 
            { 
                new Cell(new Vector2Int(0, 0), Cell.CellType.Vanish),
                new Cell(new Vector2Int(0, 1), Cell.CellType.Normal),
                new Cell(new Vector2Int(0, 2), Cell.CellType.Normal),
                new Cell(new Vector2Int(0, 3), Cell.CellType.Vanish),
            }, out int count);

            Debug.Log(count);
        }
        
        [ContextMenu("GenerateItem")]
        private void GenerateGrids()
        {
            m_grids.ForEach(x => DestroyImmediate(x));

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