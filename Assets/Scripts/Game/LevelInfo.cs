using System;
using Database;

namespace Game
{
    public class LevelInfo : IComparable<LevelInfo>
    {
        public enum StarsCount
        {
            ZeroStar = 0,
            OneStar = 1,
            TwoStar = 2,
            ThreeStar = 3,
        }

        public enum Type
        {
            Story,
            Custom
        }

        [PrimaryKey] public int ID { get; set; }

        public string LevelName { get; set; }
        public string Seed { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int startX = -1, startY = -1;
        public int endX = -1, endY = -1;

        public float expectedTime;
        public float Time { get; set; } = -1f;
        public StarsCount Stars { get; set; }

        public Type LevelType { get; set; }

        public static int StarsAchieved(float achievedTime, float expectedTime)
        {
            if (achievedTime <= expectedTime) return 3;
            if (achievedTime <= expectedTime * 1.6f) return 2;
            if (achievedTime <= expectedTime * 2.5f) return 1;
            return 0;
        }

        public int CompareTo(LevelInfo other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return ID.CompareTo(other.ID);
        }
    }
}