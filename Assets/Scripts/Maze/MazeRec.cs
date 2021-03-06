using System;
using System.Collections.Generic;

namespace MazeGen
{
    /// <summary>
    /// Recursive Backtracking - Maze Generation
    /// </summary>
    public class MazeRec : Maze
    {
        public MazeRec(List<List<Node>> nodes)
            : base(nodes)
        {
        }

        /// <summary>
        /// Initialize a new 2d array of nodes
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="seed"></param>
        public MazeRec(int width, int height, int seed)
            : base(width, height, seed)
        {
        }

        public override void Generate()
        {
            int visitedCount = 1;
            int total = this.Nodes.Count * this.Nodes[0].Count;
            Stack<Node> visitedCell = new Stack<Node>();

            //Node current = this.Nodes[r.Next(this.Nodes.Count-1)][r.Next(this.Nodes[0].Count-1)];
            Node current = this.Nodes[(int)(_random.NextDouble() * this.Nodes.Count * 10) % this.Nodes.Count][(int)(_random.NextDouble() * this.Nodes[0].Count * 10) % this.Nodes.Count];
            current.IsStart = true;

            //Node end = this.End.X == -1 ? this.Nodes[(int)(r.NextDouble() * this.Nodes.Count * 10) % this.Nodes.Count][(int)(r.NextDouble() * this.Nodes[0].Count * 10) % this.Nodes.Count] : this.Nodes[this.End.X][this.End.Y];
            //end.isEnd = true;

            while (visitedCount < total)
            {
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
                    current = visitedCell.Pop();
                    current.IsBacktracked = true;
                    OnProgressChanged(visitedCount, total);
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
                visitedCell.Push(neighbour);
                current = neighbour;
                visitedCount++;

                OnProgressChanged(visitedCount, total);
            }

            //Backtrack to start point
            while (visitedCell.Count>0)
            {
                 current = visitedCell.Pop();
                 current.IsBacktracked = true;
                 OnProgressChanged(visitedCount, total);
            }

            OnComplete();
        }

        public override string Name
        {
            get
            {
                return "Recursive Backtracker";
            }
        }
    }
}
