using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER.Score
{
    public class SimpleScoreCalculator : MonoBehaviour, IScoreCalculator
    {
        [SerializeField] int m_scoreRate=500;
        int m_nowScoreSum = 0;
        public int CalculateScore(int deleteCount)
        {
            var addScore= m_scoreRate * deleteCount;
            m_nowScoreSum += addScore;
            return addScore;
        }

        public void Reset()
        {
            m_nowScoreSum = 0;
        }
    }
}