namespace TETORIEVER.Score
{
    public interface IScoreCalculator
    {
        void Reset();
        int CalculateScore(int deleteCount);
    }
}