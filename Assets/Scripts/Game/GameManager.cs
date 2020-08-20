using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            if (Settings == null)
            {
                //if settings are null, then the app has never been run... 
                //so create settings, initialize db, and save settings to db
                Settings = new Settings();

                Settings.controlType = Settings.GetControlType().Item1.binding;

                DatabaseHandler.CreateSettings(Settings);
            }

            //todo get levels from db,
            StoryLevels = DatabaseHandler.GetStoryLevels();

            if (StoryLevels == null || StoryLevels.Count <= 0)
            {
                //there are no levels in the db
                StoryLevels = Levels();
                //put in db
                DatabaseHandler.CreateStoryLevels(StoryLevels);
            }

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

        private List<LevelInfo> Levels()
        {
            List<LevelInfo> Levels = new List<LevelInfo>();
            var levels = Resources.Load<TextAsset>("Levels/Story");

            var lines = levels.text.Split('\n');

            foreach (var line in lines)
            {
                if (line == String.Empty) break;
                var data = line.Split(',');
                Levels.Add(new LevelInfo()
                {
                    id = Int32.Parse(data[0]),
                    levelName = data[1],
                    width = Int32.Parse(data[2]),
                    height = Int32.Parse(data[3]),
                    seed = data[4].Trim('\r'),
                });
            }

            return Levels;
        }
    }
}