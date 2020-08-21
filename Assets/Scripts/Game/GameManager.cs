using System;
using System.Collections.Generic;
using Database;
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

        [SerializeField] public Sprite stars0, stars1, stars2, stars3;

        public static Settings Settings;

        private void Awake()
        {
            if (gameManagerRef == null) gameManagerRef = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            
            Settings = DatabaseHandler.GetSettings();
            
            //todo get levels from db,
            StoryLevels = DatabaseHandler.GetStoryLevels();
            //load main menu
            GoToMain();
        }

        public void GoToMain()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void GoToSettings()
        {
            SceneManager.LoadScene("Settings");
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

    }
}