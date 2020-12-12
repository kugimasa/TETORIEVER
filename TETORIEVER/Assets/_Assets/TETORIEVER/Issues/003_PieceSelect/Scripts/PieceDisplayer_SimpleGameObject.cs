using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER.PieceSelect
{
    //画面表示をGameObjectとして行うもの
    //IPieceDisplayerを継承すれば置き換え可能　UIコンテンツ(Imageなど)で作る可能性あり？
    public class PieceDisplayer_SimpleGameObject : MonoBehaviour,IPieceDisplayer
    {
        [Header("prefab")]
        [SerializeField] private GameObject m_normalKomaPrefab;
        [SerializeField] private GameObject m_vanishKomaPrefab;
        [Header("キャッシュ")]
        [SerializeField] private Transform m_PieceAnchor;
        [Header("offset")]
        [SerializeField] private Vector3 m_offsetX = Vector3.right;
        [SerializeField] private Vector3 m_offsetY = Vector3.forward;
        [SerializeField] private Vector3 m_offsetUp = default;//どれくらい浮かすか


        private List<GameObject> m_myMasuBlocks = new List<GameObject>();

        public void CreatePiece(PieceData blockData)
        {
            m_myMasuBlocks.ForEach(x => Destroy(x));
            m_myMasuBlocks = new List<GameObject>();

            var centerIndex = new Vector2Int(1,1);
            for(int x = 0; x < blockData.BlockData.GetLength(0); x++)
            {
                for(int y = 0; y < blockData.BlockData.GetLength(1); y++)
                {
                    var celltype = blockData.BlockData[x, y].m_cellType;
                    if (celltype == Cell.CellType.None ||celltype== Cell.CellType.Wall) continue;

                    var creteIndex = new Vector2Int(x, y) - centerIndex;
                    var createPosition = m_offsetX * creteIndex.x + m_offsetY * creteIndex.y;
                    var targetPrefab = (celltype == Cell.CellType.Vanish) ? m_vanishKomaPrefab : m_normalKomaPrefab;
                    var obj=Instantiate(targetPrefab,Vector2.zero, Quaternion.identity, m_PieceAnchor);
                    obj.transform.localPosition = createPosition;
                    m_myMasuBlocks.Add(obj);
                }
            }
        }

        public void SetPosition(Vector3 centerPosition)
        {
            m_PieceAnchor.position = centerPosition+m_offsetUp;
        }
    }
}