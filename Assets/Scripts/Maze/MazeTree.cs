using System;
using System.Collections.Generic;
using System.Linq;
namespace MazeGen
{
    public class MazeTree : Maze
    {
        
        public MazeTree(List<List<Node>> nodes)
            : base(nodes)
        {
        }

        /// <summary>
        /// Initialize a new 2d array of nodes, and seed the random generator
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="seed"></param>
        public MazeTree(int width, int height, int seed)
            : base(width, height, seed)
        {
        }

        public override void Generate()
        {
            int total = this.Nodes.Count * this.Nodes[0].Count;
            int visited = 1;


            List<Node> cells = new List<Node>();
            cells.Add(this.Nodes[(int)(_random.NextDouble() * 10) % this.Nodes.Count][(int)(_random.NextDouble() * 10) % this.Nodes[0].Count]);
            cells[0].IsStart = true;

            //Selection method
            //0 = Lastest
            //1 = Oldest
            //2 = Random

            //default = 0
            Func<Node> selMethod = () =>
                        {
                            return cells.Last();
                        };
            switch (this.SelectionMethod)
            {
                case 1:
                    selMethod = () =>
                        {
                            return cells[0];
                        };
                    break;
                case 2:
                    selMethod = () =>
                        {
                            return cells[(int)(_random.NextDouble() * 10) % cells.Count];
                        };
                    break;
            }

            while (cells.Count > 0)
            {
                Node current = selMethod();

                //List all available neighbour cells
                List<Node> readyNeighbourCells = new List<Node>();
                //Store the index of the neighbour cells
                List<int> readyNeighbourCellsIndex = new List<int>();
                for (int i = 0; i < current.Count; i++)
                {
                    if (current[i] != null && current[i].Wall == 0)
                    {
                        readyNeighbourCells.Add(current[i]);
                        readyNeighbourCellsIndex.Add(i);
                    }
                }

                //no cells found
                if (readyNeighbourCells.Count == 0)
                {
                    current.IsBacktracked = true;
                    cells.Remove(current);
                    OnProgressChanged(visited, total);
                    continue;
                }

                //Random select a cell
                int randIndex = (int)(_random.NextDouble() * 10) % readyNeighbourCells.Count;
                int index = readyNeighbourCellsIndex[randIndex];
                Node neighbour = readyNeighbourCells[randIndex];

                // Knock the wall
                // 0-2 1-3
                current.UnWall(index);
                neighbour.UnWall((index + 2) % 4);
                OnProgressChanged(visited, total);
                cells.Add(neighbour);
                visited++;
            }

            OnComplete();
        }

        public override string Name
        {
            get
            {
                return "Growing Tree Algorithm";
            }
        }

    }
}
