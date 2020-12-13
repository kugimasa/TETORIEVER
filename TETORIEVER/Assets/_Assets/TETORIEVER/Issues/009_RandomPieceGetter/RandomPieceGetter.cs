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
        [SerializeField] private GameEventStringListener m_chengePiece;
        [SerializeField] private GameEventListener m_putPiece;

        int m_nowSelectIndex = 0;

        Cell[][] m_holdCells = new Cell[4][];

        Cell[][] m_cellDatas=null;

        IPieceSource pieceSource = new SimplePieceSource();

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

        private void Awake()
        {
            Services.PieceGetter = this;
            m_chengePiece.Subscribe(ChengeIndex).DisposeOnSceneUnLoaded();
            m_putPiece.Subscribe(PutEvent).DisposeOnSceneUnLoaded();

            if (TryGetComponent<IPieceSource>(out var ip)) pieceSource = ip;
            m_cellDatas = pieceSource.PieceDatas;
        }

        void ChengeIndex(string i)
        {
            switch (i)
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