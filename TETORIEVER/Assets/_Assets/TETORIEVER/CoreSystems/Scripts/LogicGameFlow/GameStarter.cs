using System;
using System.Collections;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Singletons;
using UnityEngine.Events;

namespace TETORIEVER
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private GameObject m_gameManger = default;
        [SerializeField] private GameEventListener m_onPlayFinished = default;
        [SerializeField] private string m_nextScene = default;
        [SerializeField] private UnityEvent m_onStart = default;

        private IDisposable m_disposable = default;

        private IEnumerator Start()
        {
            m_onStart?.Invoke();
            m_disposable = m_onPlayFinished?.Subscribe(OnPlayFInished);
            
            yield return Services.StartEffect?.Play();

            m_gameManger?.SetActive(true);
        }

        private void OnDestroy()
        {
            m_disposable?.Dispose();
        }

        private void OnPlayFInished()
        {
            StartCoroutine(EndCoroutine());
        }

        private IEnumerator EndCoroutine()
        {
            yield return Services.ResultView?.Show();
            yield return SceneLoader.WaitLoading;
            SceneLoader.LoadScene(m_nextScene);
        }
    }
}