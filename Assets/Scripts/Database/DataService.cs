using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;
using Database;
using Game;

namespace Database
{
    public class DataService
    {
        private SQLiteConnection _connection;

        public DataService(string DatabaseName)
        {
#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb =
 new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb =
 Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb =
 Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb =
 Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb =
 Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb =
 Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            Debug.Log("Final PATH: " + dbPath);
        }

        public void CreateDB()
        {
            _connection.DropTable<LevelInfo>();
            _connection.CreateTable<LevelInfo>();

            //todo add levels from csv

            _connection.DropTable<Settings>();
            _connection.CreateTable<Settings>();

            _connection.Insert(new Settings());
        }

        public IEnumerable<LevelInfo> GetLevels()
        {
            return _connection.Table<LevelInfo>();
        }

        public IEnumerable<LevelInfo> GetLevels(LevelInfo.Type levelType)
        {
            return _connection.Table<LevelInfo>().Where(l => l.LevelType == levelType);
        }

        public Settings GetSettings()
        {
            return _connection.Table<Settings>().FirstOrDefault() ?? new Settings();
        }

        public void PutLevels(List<LevelInfo> levels)
        {
            _connection.InsertAll(levels);
        }

        public void UpdateLevel(LevelInfo currentLevel)
        {
            _connection.Update(currentLevel);
        }

        public void Close()
        {
            _connection.Dispose();
        }
    }
}