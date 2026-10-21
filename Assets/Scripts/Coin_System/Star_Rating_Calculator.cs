namespace MaterialShift
{
    public static class Star_Rating_Calculator
    {
        public static int CalculateRating(Coin_Tracker coinTracker)
        {
            if (coinTracker == null || coinTracker.TotalCount <= 0)
            {
                return 0;
            }

            int totalCount = coinTracker.TotalCount;
            int collectedCount = UnityEngine.Mathf.Clamp(
                coinTracker.CollectedCount,
                0,
                totalCount);

            if (collectedCount == totalCount)
            {
                return 3;
            }

            long collectedPercent = (long)collectedCount * 100L;
            long totalForThreshold = (long)totalCount;

            if (collectedPercent >= totalForThreshold * 70L)
            {
                return 2;
            }

            if (collectedPercent >= totalForThreshold * 30L)
            {
                return 1;
            }

            return 0;
        }
    }
}