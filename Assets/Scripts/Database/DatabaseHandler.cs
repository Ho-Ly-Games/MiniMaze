using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif

namespace Database
{
    public class DatabaseHandler : MonoBehaviour
    {
        private static DataService database;

        private string dbName = "MiniMaze.db";

        private void OnEnable()
        {
            //todo, check if file exists
#if !UNITY_EDITOR
            // check if file exists in Application.persistentDataPath
            var filepath = string.Format("{0}/{1}", Application.persistentDataPath, dbName);
            var exists = true;            
            if (!File.Exists(filepath))
            {
                exists = false;
            }
#endif

            database = new DataService(dbName);

#if UNITY_EDITOR
            database.CreateDB();
#else
            if (!exists){
                database.CreateDB();
            }
#endif
        }

        private void OnDisable()
        {
            database.Close();
            database = null;
        }

        public static Settings GetSettings()
        {
            return database.GetSettings();
        }

        public static void UpdateSettings(Settings settings)
        {
            database.PutSettings(settings);
        }

        public static List<LevelInfo> GetStoryLevels()
        {
            var levels = database.GetLevels(LevelInfo.Type.Story).ToList();
            if (levels.Count == 0)
            {
                var levelAsset = Resources.Load<TextAsset>("Levels/Story");

                var lines = levelAsset.text.Split('\n');

                foreach (var line in lines)
                {
                    if (line == String.Empty) break;
                    var data = line.Split(',');
                    levels.Add(new LevelInfo()
                    {
                        ID = Int32.Parse(data[0]),
                        LevelName = data[1],
                        Width = Int32.Parse(data[2]),
                        Height = Int32.Parse(data[3]),
                        Seed = data[4].Trim('\r'),
                        LevelType = LevelInfo.Type.Story
                    });
                }

                database.PutLevels(levels);
            }

            return levels;
        }

        public static void UpdateLevel(LevelInfo currentLevel)
        {
            database.UpdateLevel(currentLevel);
        }

        public static void ResetDB()
        {
            database.CreateDB();
            GameManager.StoryLevels = database.GetLevels(LevelInfo.Type.Story).ToList();
        }
    }
}