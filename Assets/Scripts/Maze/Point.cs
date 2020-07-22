namespace MazeGen
{
    public struct Point
    {
        private int _x;

        public int X
        {
            get { return _x; }
        }

        private int _y;

        public int Y
        {
            get { return _y; }
        }


        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}