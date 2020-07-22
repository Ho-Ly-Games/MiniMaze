using System;
using System.Collections.Generic;

namespace MazeGen
{
    public abstract class Maze : IDisposable
    {
        
        public enum Status
        {
            Starting, 
            InProgress,
            Complete
        }

        public Status status;
        
        public Action<int, int> ProgressChanged;
        public Action Completed;
        
        internal Random _random;
        private int _selIndex = 0;
        private List<List<Node>> _nodes = new List<List<Node>>();

        public Maze(List<List<Node>> nodes)
        {
            _nodes = nodes;
        }

        /// <summary>
        /// Initialize a new 2d array of nodes
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="seed"></param>
        public Maze(int width, int height, int seed)
        {
            _random= new Random(seed);
            for (int x = 0; x < width; x++)
            {
                List<Node> nX = new List<Node>();
                for (int y = 0; y < height; y++)
                {
                    Node nY = new Node();
                    nY.Pos = new Point(x, y);
                    if (y > 0)
                    {
                        nY.Up = nX[y - 1];
                        nX[y - 1].Down = nY;
                    }
                    if (x > 0)
                    {
                        nY.Left = _nodes[x - 1][y];
                        _nodes[x - 1][y].Right = nY;
                    }
                    nX.Add(nY);
                }
                _nodes.Add(nX);
            }
        }

        protected virtual void OnProgressChanged(int done, int total)
        {
            ProgressChanged?.Invoke(done, total);
        }

        protected virtual void OnComplete()
        {
            Completed?.Invoke();
        }

        /// <summary>
        /// Generate a new maze
        /// </summary>
        public virtual void Generate()
        {
        }

        /// <summary>
        /// Get the 2d list of nodes
        /// </summary>
        public List<List<Node>> Nodes
        {
            get
            {
                return _nodes;
            }
        }

        public virtual string Name
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// For Growing Tree Algorithm
        /// </summary>
        public int SelectionMethod
        {
            get
            {
                return _selIndex;
            }
            set
            {
                _selIndex = value;
            }
        }

        public void Dispose()
        {
            
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _nodes = null;
            }
        }
    }
}