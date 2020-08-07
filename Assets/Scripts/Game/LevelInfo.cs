﻿using System;

namespace Game
{
    public class LevelInfo
    {
        public enum StarsCount
        {
            ZeroStar = 0,
            OneStar = 1,
            TwoStar = 2,
            ThreeStar = 3,
        }

        public int id;

        public string levelName;
        public string seed;
        public int width, height;

        public int startX = -1, startY = -1;
        public int endX = -1, endY = -1;

        public float expectedTime;
        public float time = Single.PositiveInfinity;
        public StarsCount stars;

        public static int StarsAchieved(float achievedTime, float expectedTime)
        {
            if (achievedTime <= expectedTime) return 3;
            if (achievedTime <= expectedTime * 1.6f) return 2;
            if (achievedTime <= expectedTime * 2.5f) return 1;
            return 0;
        }
    }
}