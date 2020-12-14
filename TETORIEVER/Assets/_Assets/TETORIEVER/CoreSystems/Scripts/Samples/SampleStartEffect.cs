using System.Collections;
using UnityEngine;

namespace TETORIEVER
{
    public class SampleStartEffect : MonoBehaviour, IStartEffect
    {
        [SerializeField] private GameObject m_gameObject = default;

        private void Awake()
        {
            Services.StartEffect = this;
            m_gameObject.SetActive(false);
        }

        public IEnumerator Play()
        {
            m_gameObject.SetActive(true);
            yield return new WaitUntil(() => Input.anyKeyDown);
            m_gameObject.SetActive(false);
        }
    }
}