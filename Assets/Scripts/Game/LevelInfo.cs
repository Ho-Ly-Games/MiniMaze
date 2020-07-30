namespace Game
{
    public class LevelInfo
    {
        public enum StartCount
        {
            ZeroStar,
            OneStar,
            TwoStar,
            ThreeStar,
        }

        public string levelName;
        public string seed;
        public int width, height;

        public int startX, startY;
        public int endX, endY;

        public float expectedTime;
        public float time;
        public StartCount stars;
    }
}