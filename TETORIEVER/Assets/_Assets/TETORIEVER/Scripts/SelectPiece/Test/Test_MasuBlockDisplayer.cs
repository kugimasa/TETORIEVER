using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER.PieceSelect
{
    public class Test_MasuBlockDisplayer : MonoBehaviour
    {
        [SerializeField] private PieceDisplayer_SimpleGameObject m_displayer;
        [SerializeField] private Vector2 setPosition;
        [SerializeField] private HandPieceHolder m_nowMasuBlockHolder;
        [SerializeField] private GameEventString m_inputEvent;

        

        [ContextMenu("createBlock")]
        private void Test_createBlock()
        {
            var blockData=new PieceData(new Cell[,] {
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal) },
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.Vanish),new Cell(Vector2Int.zero,Cell.CellType.None) }
            });
            m_displayer.CreatePiece(blockData);
        }

        [ContextMenu("setPosition")]
        private void Test_setPosition()
        {
            m_displayer.SetPosition(setPosition);
        }

        [ContextMenu("createNowMasuBlockHolder")]
        private void Test_createNowMasuBlockHolder()
        {
            m_nowMasuBlockHolder.SetPieceData(
                new PieceData(new Cell[,] {
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal) },
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.Vanish),new Cell(Vector2Int.zero,Cell.CellType.None) }
            }), 0);
            m_nowMasuBlockHolder.SetPieceData(
                new PieceData(new Cell[,] {
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.Vanish),new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None) }
            }), 1);
            m_nowMasuBlockHolder.SetPieceData(
                new PieceData(new Cell[,] {
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None) }
            }), 2);
            m_nowMasuBlockHolder.SetPieceData(
                new PieceData(new Cell[,] {
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Vanish) },
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None) }
            }), 3);
        }

    }
}