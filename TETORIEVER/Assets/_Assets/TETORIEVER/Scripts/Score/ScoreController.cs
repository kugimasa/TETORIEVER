using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER.Score
{
    public class ScoreController : MonoBehaviour
    {
        [Header("ScoreCaluculator")]
        [SerializeField] GameObject m_ScoreCalculateObject;//IScoreCalculatorをgetComponentできるオブジェクト
        IScoreCalculator m_ScoreCalculator;

        [Header("重要Variable")]
        [SerializeField] GameEventIntListener m_OnCellDeletedListener;
        [SerializeField] VariableInt m_Score;
        [SerializeField] VariableInt m_highScore;


        [Header("その他Variable")]//初期化のタイミングなど
        [SerializeField] GameEventListener m_PlayStartInitializeListener;//プレイ開始時の初期化を通知するイベント　プレイ毎に呼ばれてほしい
        [SerializeField] GameEventListener m_UpdateHighScoreListener;//ハイスコアの更新を行うイベント

        [Header("設定")]
        [SerializeField] bool m_UpdateHighScoreOnPlaying=false;//プレイ中にハイスコアの更新を行うかどうか

        private void Start()
        {
            m_ScoreCalculator = m_ScoreCalculateObject.GetComponent<IScoreCalculator>();
            m_OnCellDeletedListener.Subscribe((num) =>
            {
                UpdateScore(num);
                if (m_UpdateHighScoreOnPlaying) UpdateHighScore();
            });

            m_PlayStartInitializeListener.Subscribe(PlayStartInitialize);
            if(!m_UpdateHighScoreOnPlaying)m_UpdateHighScoreListener.Subscribe(UpdateHighScore);
        }
        //プレイ開始時の初期化
        void PlayStartInitialize()
        {
            m_Score.Value = 0;
        }
        //スコアの更新
        //スコアの増加量はIScoreCalculatorが決定する　後からコンボやらボーナスやら入れられるように
        void UpdateScore(int deleteCount)
        {
            if (deleteCount <= 0) return;
            var addScore=m_ScoreCalculator.CalculateScore(deleteCount);
            m_Score.Value += addScore;
        }
        //highScoreの更新
        void UpdateHighScore()
        {
            if (m_highScore.Value < m_Score) m_highScore.Value = m_Score.Value;
        }
    }
}