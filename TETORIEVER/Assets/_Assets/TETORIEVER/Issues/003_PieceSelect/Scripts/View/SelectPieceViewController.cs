using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using System;

namespace TETORIEVER.PieceSelect
{
    public class SelectPieceViewController : MonoBehaviour
    {
        [SerializeField]private GameEventStringListener m_onPieceChenged;
        [SerializeField]private List<GameObject> m_DisplaySelectObjects;
        private List<IDisplaySelected> m_IDisplaySelectList = new List<IDisplaySelected>();

        private List<IDisposable> m_disposeList=new List<IDisposable>();
        
        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            m_disposeList.Clear();
            m_disposeList.Add(m_onPieceChenged.Subscribe((select) =>
            {
                SetSelect(int.Parse(select));
            }));
        }

        private void OnDisable()
        {
            m_disposeList.ForEach(x => x.Dispose());
            m_disposeList.Clear();
        }
        void Initialize()
        {
            m_IDisplaySelectList = new List<IDisplaySelected>();
            m_DisplaySelectObjects.ForEach(x => m_IDisplaySelectList.Add(x.GetComponent<IDisplaySelected>()));

        }
        void SetSelect(int selectIndex)
        {
            for(int i = 0; i < m_IDisplaySelectList.Count; i++)
            {
                var target = m_IDisplaySelectList[i];
                if (i == selectIndex)target.Select();
                else target.DisSelect();
            }
        }
    }
}