using UnityEngine;

namespace TETORIEVER.Score
{
    // 2020.12.12: code review by SilCil.
    // 【表記】
    // スペースの有無（=の両端や括弧の中身など）がばらばらなので統一.
    // privateを明示.
    // 不要なusingの削除.
    // 【処理】
    // m_nowScoreSumを削除. 必要になったときに変数追加でよいと思う.
    public class SimpleScoreCalculator : MonoBehaviour, IScoreCalculator
    {
        [SerializeField] private int m_scoreRate = 500;

        public int CalculateScore(int deleteCount)
        {
            var addScore = m_scoreRate * deleteCount;
            return addScore;
        }

        public void Reset() { }
    }
}