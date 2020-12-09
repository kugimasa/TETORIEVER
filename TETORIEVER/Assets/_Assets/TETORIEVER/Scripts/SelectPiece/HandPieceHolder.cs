using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER.PieceSelect
{
    public class HandPieceHolder : MonoBehaviour,ISetPieceData,IGetPieceData
    {
        [SerializeField] private List<GameObject> m_displayerObjectList;
        private List<IPieceDisplayer> m_displayerList;
        private List<PieceData> m_blockDataList = new List<PieceData>();

        private void Awake()
        {
            Initialize();
        }
        private void Initialize()
        {
            m_blockDataList = new List<PieceData>();
            for(int i = 0; i < 4; i++)
            {
                m_blockDataList.Add(null);
            }
            m_displayerList = new List<IPieceDisplayer>();
            m_displayerObjectList.ForEach(x => m_displayerList.Add(x.GetComponent<IPieceDisplayer>()));
        }

        public void SetPieceData(PieceData data,int hand)
        {
            m_blockDataList[hand] = data;
            m_displayerList[hand].CreatePiece(m_blockDataList[hand]);
        }

        public PieceData GetPieceData(int hand)
        {
            return m_blockDataList[hand];
        }
    }
}