using System;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER.Score
{
    // 2020.12.12: code review by SilCil.
    // 【表記】
    // privateが明示されていたりしなかったりなので統一.
    // スペースの有無（=の両端や括弧の中身など）がばらばらなので統一.
    // メンバの表記揺れがあるので統一.（m_の後を小文字に）
    // 変数はdefaultを代入しておくとコンパイラの警告がなくなる.
    // "スコアの更新"など英語を訳しただけのコメントは行数が増えるだけなので削除.
    // 不要なusingの削除.
    // イベント系をon~過去分詞系に名前統一. これは好みだと思う.
    // 【処理】
    // 空のStartメソッドを削除. （MonoBehaviourのStartやUpdateは空でも呼ばれてしまうのでパフォーマンス的にもよくない）
    // IDisposableをまとめるクラスを用意しています. お好みでどうぞ.
    // m_UpdateHighScoreOnPlayingを削除. ハイスコア更新したくない場面が想像できない... 必要になったら修正でもよいと思う.
    // ハイスコア更新用のイベントを削除. ハイスコア更新を単体で呼び出したい場面が想像できなかった. Scoreの初期化/更新タイミングで十分だと思う.
    // UpdateScoreメソッド内でHighScoreメソッドを呼び出すようにした. OnEnableがスリムになる.
    // IScoreCalculatorのResetメソッドが呼ばれていないので追加.
    public class ScoreController : MonoBehaviour
    {
        [Header("ScoreCaluculator")]
        [SerializeField] private GameObject m_scoreCalculateObject = default; // IScoreCalculatorをGetComponentできるオブジェクト.
        private IScoreCalculator m_scoreCalculator = default;

        [Header("重要Variable")]
        [SerializeField] private GameEventIntListener m_onCellDeletedListener = default;
        [SerializeField] private VariableInt m_score = default;
        [SerializeField] private VariableInt m_highScore = default;

        [Header("その他Variable")]
        [SerializeField] private GameEventListener m_onPlayStartedListener = default;

        private IDisposable m_disposable = default;

        private void OnEnable()
        {
            m_scoreCalculator = m_scoreCalculateObject.GetComponent<IScoreCalculator>();
            
            var dispose = new CompositeDisposable();
            dispose.Add(m_onCellDeletedListener?.Subscribe(UpdateScore));
            dispose.Add(m_onPlayStartedListener?.Subscribe(PlayStartInitialize));
            m_disposable = dispose;
        }

        private void OnDisable()
        {
            m_disposable?.Dispose();
        }

        private void PlayStartInitialize()
        {
            m_score.Value = 0;
            m_scoreCalculator.Reset();
        }

        private void UpdateScore(int deleteCount)
        {
            if (deleteCount <= 0) return;
            var addScore = m_scoreCalculator.CalculateScore(deleteCount);
            m_score.Value += addScore;
            UpdateHighScore();
        }

        private void UpdateHighScore()
        {
            if (m_highScore < m_score) m_highScore.Value = m_score;
        }
    }
}