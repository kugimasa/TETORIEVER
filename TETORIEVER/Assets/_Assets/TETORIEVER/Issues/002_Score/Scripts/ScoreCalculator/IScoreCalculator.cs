namespace TETORIEVER.Score
{
    // 2020.12.12: code review by SilCil.
    // 【表記】
    // インターフェースのコメントを追加.
    // XMLコメントで打っておくと実装するときに便利. (Riderはどうなってるか知らない)

    /// <summary>スコアの増加量を決定するためのインターフェース</summary>
    public interface IScoreCalculator
    {
        /// <summary>プレイ開始時に呼ばれる</summary>
        void Reset();

        /// <returns>スコアの増加量</returns>
        /// <param name="deleteCount">消した駒の数</param>
        int CalculateScore(int deleteCount);
    }
}