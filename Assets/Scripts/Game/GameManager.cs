using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager gameManagerRef;

        public static LevelInfo currentLevel;

        public static List<LevelInfo> StoryLevels;
        public static List<LevelInfo> CustomLevels;

        private void Awake()
        {
            if (gameManagerRef == null) gameManagerRef = this;
            DontDestroyOnLoad(this.gameObject);
            StoryLevels = GameLevels();
        }

        private void Start()
        {
            //load main menu
            GoToMain();
        }

        public void GoToMain()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void PlayLevel(LevelInfo level)
        {
            currentLevel = level;
            SceneManager.LoadScene("Level");
        }

        public void PlayNextLevel()
        {
            if (StoryLevels.Contains(currentLevel))
            {
                var index = StoryLevels.IndexOf(currentLevel);
                if (index + 1 >= StoryLevels.Count)
                    GoToMain();
                else
                    PlayLevel(StoryLevels[index + 1]);
            }
        }

        public void LoadLevelView()
        {
            SceneManager.LoadScene("LevelSelect");
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }

        public static List<LevelInfo> GameLevels()
        {
            List<LevelInfo> levels = new List<LevelInfo>()
            {
                new LevelInfo()
                {
                    levelName = "Level 1",
                    id = 1,
                    width = 2,
                    height = 2,
                    seed = "716065106"
                },
                new LevelInfo()
                {
                    levelName = "Level 2", id = 2,
                    width = 3,
                    height = 3,
                    seed = "997368232"
                },
                new LevelInfo()
                {
                    levelName = "Level 3",
                    id = 3,
                    width = 4,
                    height = 4,
                    seed = "1139115468"
                },
                new LevelInfo()
                {
                    levelName = "Level 4",
                    id = 4,
                    width = 5,
                    height = 5,
                    seed = "2040860138"
                },
                new LevelInfo()
                {
                    levelName = "Level 5",
                    id = 5,
                    width = 6,
                    height = 6,
                    seed = "1998986992"
                },
                new LevelInfo()
                {
                    levelName = "Level 6",
                    id = 6,
                    width = 7,
                    height = 7,
                    seed = "453579586"
                },
                new LevelInfo()
                {
                    levelName = "Level 7",
                    id = 7,
                    width = 8,
                    height = 8,
                    seed = "1826015642"
                },
                new LevelInfo()
                {
                    levelName = "Level 8",
                    id = 8,
                    width = 9,
                    height = 9,
                    seed = "1678668641"
                },
                new LevelInfo()
                {
                    levelName = "Level 9",
                    id = 9,
                    width = 10,
                    height = 10,
                    seed = "446943417"
                },
                new LevelInfo()
                {
                    levelName = "Level 10",
                    id = 10,
                    width = 10,
                    height = 10,
                    seed = "599432958"
                },
                new LevelInfo()
                {
                    levelName = "Level 11",
                    id = 11,
                    width = 10,
                    height = 10,
                    seed = "199168"
                },
                new LevelInfo()
                {
                    levelName = "Level 12",
                    id = 12,
                    width = 10,
                    height = 10,
                    seed = "1892745453"
                },
                new LevelInfo()
                {
                    levelName = "Level 13",
                    id = 13,
                    width = 12,
                    height = 12,
                    seed = "964050918"
                },
                new LevelInfo()
                {
                    levelName = "Level 14",
                    id = 14,
                    width = 12,
                    height = 12,
                    seed = "288009126"
                },
                new LevelInfo()
                {
                    levelName = "Level 15",
                    id = 15,
                    width = 15,
                    height = 15,
                    seed = "1810818217"
                },
            };
            return levels;
        }
    }
}