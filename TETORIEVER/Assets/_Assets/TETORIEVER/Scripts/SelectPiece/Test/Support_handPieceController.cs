using SilCilSystem.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TETORIEVER.PieceSelect
{
    /// <summary>
    /// 最終的には外す　デバッグパラメータをすべてfalseにすれば無効化できる
    /// 現状の役割は以下の通り
    /// ・最初に各BlockDataDisplayrにブロックデータを渡す <-ランダム生成ができたら置き換え
    /// ・入力を受け取ってイベントを呼ぶ　<-入力部分の実装の代替
    /// インターフェースやらで定義した方がスムーズに行きそうだが力尽きた
    /// </summary>
    public class Support_handPieceController : MonoBehaviour
    {
        [SerializeField] GameObject m_SetBlockDataObject;
        ISetPieceData m_iSetBlockData;
        [SerializeField] GameEventString m_inputEvent;
        [Header("デバッグパラメータ")]
        [SerializeField] bool m_CreateHandPiece = false;
        [SerializeField] bool m_debugInput = false;
        private void Start()
        {
            m_iSetBlockData = m_SetBlockDataObject.GetComponent<ISetPieceData>();
            if (m_CreateHandPiece)
            {
                Temp_createNowPiece();
            }
        }
        private void Update()
        {
            if (m_debugInput) InputUpdate();
        }
        /// ・入力を受け取ってイベントを呼ぶ
        private void InputUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Z)) m_inputEvent.Publish(InputConstants.Hand1);
            else if (Input.GetKeyDown(KeyCode.X)) m_inputEvent.Publish(InputConstants.Hand2);
            else if (Input.GetKeyDown(KeyCode.C)) m_inputEvent.Publish(InputConstants.Hand3);
            else if (Input.GetKeyDown(KeyCode.V)) m_inputEvent.Publish(InputConstants.Hand4);
        }

        /// <summary>
        /// ・最初に各BlockDataDisplayrにブロックデータを渡す <-ランダム生成ができたら置き換え
        /// ISetPieceDataのsetPieceDataをすればできる
        /// </summary>
        private void Temp_createNowPiece()
        {
            m_iSetBlockData.SetPieceData(
                new PieceData(new Cell[,] {
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal) },
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.Vanish),new Cell(Vector2Int.zero,Cell.CellType.None) }
            }), 0);
            m_iSetBlockData.SetPieceData(
                new PieceData(new Cell[,] {
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.Vanish),new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None) }
            }), 1);
            m_iSetBlockData.SetPieceData(
                new PieceData(new Cell[,] {
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None) }
            }), 2);
            m_iSetBlockData.SetPieceData(
                new PieceData(new Cell[,] {
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None) },
            {new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Normal),new Cell(Vector2Int.zero,Cell.CellType.Vanish) },
            {new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None),new Cell(Vector2Int.zero,Cell.CellType.None) }
            }), 3);

        }
    }
}
