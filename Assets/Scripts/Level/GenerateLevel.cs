using System;
using System.Collections;
using System.Threading.Tasks;
using MazeGen;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Level
{
    public class GenerateLevel : MonoBehaviour
    {

        private int _levelIndex;
        private MazePrim mazePrim;

        [SerializeField] private MazeManager _mazeManager;


        public void GenerateMaze(int width, int height, int seed)
        {
            mazePrim = new MazePrim(width, height, seed);
            StartCoroutine(MazeWatcher());
            Task.Run(mazePrim.Generate);
        }

        IEnumerator MazeWatcher()
        {
            bool running = true;
            do
            {
                yield return null;
                switch (mazePrim.status)
                {
                    case Maze.Status.Starting:
                        break;
                    case Maze.Status.InProgress:
                        break;
                    case Maze.Status.Complete:
                        //todo calculations
                         var goal = _mazeManager.FindGoal(mazePrim.Nodes);
                         var start = _mazeManager.FindStart(mazePrim.Nodes, goal.Position);
                        //todo figure where the ball goes, and where goal is
                        //ball goes into maze at 0, 0
                        //goal is farthest path to dead end
                        //todo calculate the score for the level
                        _mazeManager.CreateMaze(mazePrim.Nodes);
                        
                        //put goal at goal node
                        _mazeManager.SetReady(start, goal);
                        running = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            } while (running);
        }
    }
}