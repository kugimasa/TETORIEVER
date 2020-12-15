using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER.PieceSelect
{
    public class SelectPieceController : MonoBehaviour
    {
        [Header("キャッシュ")]
        [SerializeField]private GameObject m_holdBlockDisplayerObject;
        private IPieceDisplayer m_holdBlockDisplayer;
        [SerializeField] private GameObject m_masuBlockHolderObject;
        private IGetPieceData m_masuBlockHolder;

        [Header("ScriptableObject")]
        [SerializeField] private  GameEventStringListener m_InputListenr;
        [SerializeField] private ReadonlyVector3 m_BoardWorldPosition;
        [SerializeField] private GameEventString m_OnPieceChenged;

        //その他ローカル
        private string m_nowHoldkey = "";
        private int m_nowSelectIndex = -1;
        private List<IDisposable> m_disposeList;

        private void Awake()
        {
            m_holdBlockDisplayer = m_holdBlockDisplayerObject.GetComponent<IPieceDisplayer>();
            m_masuBlockHolder = m_masuBlockHolderObject.GetComponent<IGetPieceData>();
        }

        private void Update()
        {
            UpdateHoldBlocks();
        }

        private void OnEnable()
        {
            m_disposeList = new List<IDisposable>();
            m_disposeList.Add( m_InputListenr.Subscribe(ChengeHold));
        }

        private void OnDisable()
        {
            m_disposeList.ForEach(x => x.Dispose());
            m_disposeList = new List<IDisposable>();
        }

        private void ChengeHold(string key)
        {
            if (m_nowHoldkey == key) return;
            switch (key)
            {
                case InputConstants.Hand1:
                    m_nowSelectIndex = 0;
                    break;
                case InputConstants.Hand2:
                    m_nowSelectIndex = 1;
                    break;
                case InputConstants.Hand3:
                    m_nowSelectIndex = 2;
                    break;
                case InputConstants.Hand4:
                    m_nowSelectIndex = 3;
                    break;
                default:return;
            }

            var targetData = m_masuBlockHolder.GetPieceData(m_nowSelectIndex);
            m_holdBlockDisplayer.CreatePiece(targetData);
            m_nowHoldkey = key;

            //0～3のindexでイベント発行
            //変更が発生しないときは呼ばれない
            m_OnPieceChenged.Publish(m_nowSelectIndex.ToString());
        }

        private void UpdateHoldBlocks()
        {
            m_holdBlockDisplayer.SetPosition(m_BoardWorldPosition.Value);
        }

    }
}