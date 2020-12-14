using System.Collections;
using UnityEngine;

namespace TETORIEVER
{
    public class SampleResultView : MonoBehaviour, IResultView
    {
        [SerializeField] private GameObject m_gameObject = default;
        [SerializeField] private float m_waitTime = 1f;

        private void Start()
        {
            m_gameObject.SetActive(false);
            Services.ResultView = this;
        }

        public IEnumerator Show()
        {
            m_gameObject.SetActive(true);
            yield return new WaitForSeconds(m_waitTime);
            yield return new WaitUntil(() => Input.anyKey);
            m_gameObject.SetActive(false);
        }
    }
}