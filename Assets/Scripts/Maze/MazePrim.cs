using System;
using System.Collections.Generic;

namespace MazeGen
{
    public class MazePrim : Maze
    {
        
        public MazePrim(List<List<Node>> nodes)
            : base(nodes)
        {
        }

        /// <summary>
        /// Initialize a new 2d array of nodes
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="seed"></param>
        public MazePrim(int width, int height, int seed)
            : base(width, height, seed)
        {
        }

        public override void Generate()
        {
            status = Status.InProgress;
            
            //todo use seed
            int total = this.Nodes.Count * this.Nodes[0].Count;
            int visited = 1;

            List<Node> frontier = new List<Node>();

            Node current = this.Nodes[(int)(_random.NextDouble() * 10) % this.Nodes.Count][(int)(_random.NextDouble() * 50) % this.Nodes[0].Count];
            current.IsStart = true;
            for (int i = 0; i < current.Count; i++)
            {
                if (current[i] != null)
                {
                    current[i].ParentInfo.Add(new ParentInfo(current, i));
                    frontier.Add(current[i]);
                    current[i].IsFrontier = true;
                }
            }

            while (frontier.Count > 0)
            {
                current = frontier[(int)(_random.NextDouble() * 10) % frontier.Count];
                current.IsFrontier = false;
                frontier.Remove(current);

                //random select a parent
                ParentInfo parentInfo = current.ParentInfo[(int)(_random.NextDouble() * 15) % current.ParentInfo.Count];

                //break the wall
                //0-2 1-3
                parentInfo.Parent.UnWall(parentInfo.Index);
                current.UnWall((parentInfo.Index + 2) % 4);

                //add new frontier
                for (int i = 0; i < current.Count; i++)
                {
                    //not a frontier yet
                    //and no walls destroyed
                    if (current[i] != null && current[i].ParentInfo.Count == 0 && current[i].Wall == 0)
                    {
                        frontier.Add(current[i]);
                        current[i].ParentInfo.Add(new ParentInfo(current, i));
                        current[i].IsFrontier = true;
                    }
                }

                visited++;
                OnProgressChanged(visited, total);
            }

            OnComplete();

            status = Status.Complete;
        }

        public override string Name
        {
            get
            {
                return "Prim's Algorithm";
            }
        }
    }
}
