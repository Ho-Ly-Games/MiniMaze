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

        private static List<LevelInfo> StoryLevels;
        private static List<LevelInfo> CustomLevels;

        private void Awake()
        {
            if (gameManagerRef == null) gameManagerRef = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            //load main menu
            SceneManager.LoadScene("MainMenu");
        }

        private void PlayLevel(LevelInfo level)
        {
            currentLevel = level;
            SceneManager.LoadScene("Level");
        }

        private void PlayNextLevel()
        {
            //get current level index, load next level
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