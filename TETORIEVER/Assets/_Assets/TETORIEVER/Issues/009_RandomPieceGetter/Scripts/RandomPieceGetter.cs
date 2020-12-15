using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER
{


    public class RandomPieceGetter :MonoBehaviour, IPieceGetter
    {
        [SerializeField] private GameEventStringListener m_chengePieceEventListener;
        [SerializeField] private GameEventListener m_putPieceEventListener;


        private Cell[][] m_cellDatas = null;
        private Cell[][] m_holdCells = new Cell[4][];
        private int m_nowSelectIndex = 0;

        //ピースのデータを供給するもの
        private IPieceSource m_pieceSource = new SimplePieceSource();


        private void Awake()
        {
            Services.PieceGetter = this;

            m_chengePieceEventListener.Subscribe(ChengeIndex).DisposeOnSceneUnLoaded();
            m_putPieceEventListener.Subscribe(PutEvent).DisposeOnSceneUnLoaded();
            if (TryGetComponent<IPieceSource>(out var ip)) m_pieceSource = ip;
            m_cellDatas = m_pieceSource.PieceDatas;
        }


        public IEnumerable<Cell> GetPiece(int index)
        {
            if (m_holdCells[index] == null) m_holdCells[index] = GenerateRandom();
            return m_holdCells[index];
        }

        public void Rotate()
        {
            var targetPiece = m_holdCells[m_nowSelectIndex];
            for (int i = 0; i < targetPiece.Length; i++)
            {
                var pos = targetPiece[i].m_position;
                targetPiece[i].m_position = new Vector2Int(pos.y, -pos.x);
            }
        }

        void ChengeIndex(string input)
        {
            switch (input)
            {
                case InputConstants.Hand1:m_nowSelectIndex = 0;break;
                case InputConstants.Hand2:m_nowSelectIndex = 1; break;
                case InputConstants.Hand3:m_nowSelectIndex = 2; break;
                case InputConstants.Hand4:m_nowSelectIndex = 3; break;
            }
        }

        void PutEvent()
        {
            m_holdCells[m_nowSelectIndex] = GenerateRandom();
        }

        Cell[] GenerateRandom()
        {
            var index=UnityEngine.Random.Range(0, m_cellDatas.GetLength(0));
            return m_cellDatas[index].Clone() as Cell[];
        }
    }
}