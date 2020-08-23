using System;
using System.Collections.Generic;
using Database;
using Game;
using MazeGen;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Level
{
    public class MazeManager : MonoBehaviour
    {
        [System.Serializable]
        public struct GoalProp
        {
            public bool GoalFound;
            public Vector2Int Position;
            public float Time;
        }

        [SerializeField] private CameraController cameraController;
        [SerializeField] private Timer timer;

        [SerializeField] private Ball Ball;
        private Ball _localBall;
        [SerializeField] private Goal Goal;
        private Goal _localGoal;

        [SerializeField] private GenerateLevel generateLevel;

        [SerializeField] private List<Tile> tiles;

        [SerializeField] private Tilemap tilemap;

        [SerializeField] private GoalProp goal;
        [SerializeField] private GoalProp start;

        [SerializeField] private GameObject pauseButton;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private LevelCompleteScreen levelCompleteScreen;

        public void CreateMaze(List<List<Node>> nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes[i].Count; j++)
                {
                    tilemap.SetTile(new Vector3Int(i, (nodes[i].Count - 1) - j, 0), tiles[nodes[i][j].Wall - 1]);
                }
            }
        }

        public void Pause(bool pause)
        {
            pauseButton.SetActive(!pause);
            pauseMenu.SetActive(pause);
            Time.timeScale = pause ? 0 : 1;
        }

        public void ToMainMenu()
        {
            Pause(false);
            GameManager.gameManagerRef.GoToMain();
        }

        public void RestartLevel()
        {
            Pause(false);
            GameManager.gameManagerRef.PlayLevel(GameManager.currentLevel);
        }

        public void NextLevel()
        {
            Pause(false);
            GameManager.gameManagerRef.PlayNextLevel();
        }

        private void Start()
        {
            MakeMaze(GameManager.currentLevel);
        }

        public void MakeMaze(LevelInfo level)
        {
            generateLevel.GenerateMaze(level.Width, level.Height, level.Seed.GetHashCode());
        }

        public GoalProp FindGoal(List<List<Node>> nodes)
        {
            _visited = new bool[nodes.Count, nodes[0].Count];

            return FindGoalRec(nodes, Vector2Int.zero, new GoalProp()
            {
                GoalFound = false,
                Position = Vector2Int.zero,
                Time = 0
            });
        }

        public GoalProp FindStart(List<List<Node>> nodes, Vector2Int goalPos)
        {
            _visited = new bool[nodes.Count, nodes[0].Count];

            return FindGoalRec(nodes, goalPos, new GoalProp()
            {
                GoalFound = false,
                Position = Vector2Int.zero,
                Time = 0
            });
        }

        private bool[,] _visited;

        private GoalProp FindGoalRec(List<List<Node>> nodes, Vector2Int pos, GoalProp goal)
        {
            //distance calculations
            //dead-end 1 sec
            //straight 5 sec
            //corner 7 sec
            //three-way 8 sec
            //four-way 10 sec


            //mark current as visited
            _visited[pos.x, pos.y] = true;

            //add the time of the current node to the goal time
            switch (nodes[pos.x][pos.y].Wall)
            {
                case 1:
                case 2:
                case 4:
                case 8:
                    //dead end
                    goal.Time += 0.7f;
                    break;
                case 3:
                case 6:
                case 9:
                case 12:
                    //corners
                    goal.Time += 0.2f;
                    break;
                case 5:
                case 10:
                    //straight
                    goal.Time += 0.1f;
                    break;
                case 7:
                case 11:
                case 13:
                case 14:
                    //three-way
                    goal.Time += 0.3f;
                    break;
                case 15:
                    //four-way
                    goal.Time += 0.6f;
                    break;
            }

            GoalProp
                up = new GoalProp {Time = 0, Position = Vector2Int.one * -1, GoalFound = false},
                left = new GoalProp {Time = 0, Position = Vector2Int.one * -1, GoalFound = false},
                down = new GoalProp {Time = 0, Position = Vector2Int.one * -1, GoalFound = false},
                right = new GoalProp {Time = 0, Position = Vector2Int.one * -1, GoalFound = false};
            //find each way to go that does not go through a wall, or is already visited
            if (nodes[pos.x][pos.y].GetWall(0) && !_visited[pos.x, pos.y - 1]) //up
            {
                up = FindGoalRec(nodes, new Vector2Int(pos.x, pos.y - 1), goal);
            }

            if (nodes[pos.x][pos.y].GetWall(1) && !_visited[pos.x + 1, pos.y]) //left
            {
                left = FindGoalRec(nodes, new Vector2Int(pos.x + 1, pos.y), goal);
            }

            if (nodes[pos.x][pos.y].GetWall(2) && !_visited[pos.x, pos.y + 1]) //down
            {
                down = FindGoalRec(nodes, new Vector2Int(pos.x, pos.y + 1), goal);
            }

            if (nodes[pos.x][pos.y].GetWall(3) && !_visited[pos.x - 1, pos.y]) //right
            {
                right = FindGoalRec(nodes, new Vector2Int(pos.x - 1, pos.y), goal);
            }

            //compare to find farthest
            if (up.GoalFound == false && left.GoalFound == false && down.GoalFound == false && right.GoalFound == false)
            {
                //we are in a dead end and there are no valid paths
                goal.GoalFound = true;
                goal.Position = pos;
                return goal;
            }
            else
            {
                //return farthest
                if (up.GoalFound && up.Time >= left.Time && up.Time >= right.Time && up.Time >= down.Time) return up;
                if (left.GoalFound && left.Time >= up.Time && left.Time >= right.Time && left.Time >= down.Time)
                    return left;
                if (down.GoalFound && down.Time >= left.Time && down.Time >= right.Time && down.Time >= up.Time)
                    return down;
                if (right.GoalFound && right.Time >= left.Time && right.Time >= up.Time && right.Time >= down.Time)
                    return right;
            }

            Debug.Log("No goal found");
            return goal;
        }

        public void SetReady()
        {
            var startPos3D = new Vector3(GameManager.currentLevel.startX + 0.5f,
                GameManager.currentLevel.Height - GameManager.currentLevel.startY - 0.5f, 0);

            if (_localBall == null)
                _localBall = Instantiate(Ball, startPos3D,
                    Quaternion.identity);
            else
                _localBall.transform.position = startPos3D;

            cameraController.ball = _localBall;

            var goalPos3D = new Vector3(GameManager.currentLevel.endX + 0.5f,
                GameManager.currentLevel.Height - GameManager.currentLevel.endY - 0.5f, 0);

            if (_localGoal == null)
                _localGoal = Instantiate(Goal, goalPos3D,
                    Quaternion.identity);
            else
                _localGoal.transform.position = goalPos3D;

            _localBall.goal = _localGoal;

            var tempCameraPos = cameraController.transform.position;
            tempCameraPos.x = startPos3D.x;
            tempCameraPos.y = startPos3D.y;
            cameraController.transform.position = tempCameraPos;

            _localGoal._mazeManager = this;

            timer.StartTimer();
        }

        public void Completed()
        {
            var time = timer.Stop();

            if (GameManager.currentLevel.Time < 0 || time < GameManager.currentLevel.Time)
            {
                GameManager.currentLevel.Time = time;
                GameManager.currentLevel.Stars =
                    (LevelInfo.StarsCount) LevelInfo.StarsAchieved(time, GameManager.currentLevel.expectedTime);
            }
            //GameManager.currentLevel.time = time;

            DatabaseHandler.UpdateLevel(GameManager.currentLevel);

            _localBall.rgd.velocity = Vector2.zero;
            _localBall.rgd.constraints = RigidbodyConstraints2D.FreezeAll;

            levelCompleteScreen.NextStarAt(LevelInfo.NextStarAt(time, GameManager.currentLevel.expectedTime));
            levelCompleteScreen.gameObject.SetActive(true);
        }

        public void DisablePause()
        {
            pauseButton.SetActive(false);
        }
    }
}