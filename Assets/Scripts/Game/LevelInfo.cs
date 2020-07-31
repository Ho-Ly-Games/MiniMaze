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

        public int id;

        public string levelName;
        public string seed;
        public int width, height;

        public int startX = -1, startY = -1;
        public int endX = -1, endY = -1;

        public float expectedTime;
        public float time;
        public StartCount stars;
    }
}