using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TETORIEVER
{
    public class SampleBoardEffect : MonoBehaviour, IBoardEffect
    {
        [SerializeField] private GameObject m_prefabNormal = default;
        [SerializeField] private GameObject m_prefabVanish = default;

        private Transform m_transform = default;
        private List<CellInstance> m_instances = new List<CellInstance>();
        
        private class CellInstance
        {
            public Vector2Int m_position;
            public GameObject m_instance;

            public CellInstance(Vector2Int position, GameObject instance)
            {
                m_position = position;
                m_instance = instance;
            }
        }

        private void Awake()
        {
            m_transform = transform;
            Services.BoardEffect = this;
        }

        public IEnumerator PlaceCoroutine(IEnumerable<Cell> puts)
        {
            foreach(var put in puts)
            {
                var position = Services.PositionConverter.BoardToWorldPosition(put.m_position);
                if (!position.HasValue) continue;

                var prefab = GetPrefab(put);
                if (prefab == null) continue;

                var obj = Instantiate(prefab, position.Value, Quaternion.identity, m_transform);
                m_instances.Add(new CellInstance(put.m_position, obj));
            }

            yield break;
        }

        public IEnumerator RemoveCoroutine(IEnumerable<Vector2Int> removes)
        {
            var removeInstances = removes.SelectMany(remove => m_instances.Where(x => x.m_position == remove)).ToList();
            foreach(var remove in removeInstances)
            {
                Destroy(remove.m_instance);
                m_instances.Remove(remove);
            }
            yield break;
        }

        private GameObject GetPrefab(Cell put)
        {
            switch (put.m_cellType)
            {
                case Cell.CellType.Normal: return m_prefabNormal;
                case Cell.CellType.Vanish: return m_prefabVanish;
                default: return null;
            }
        }
    }
}