using System.Collections.Generic;
using System.Data;
using Game;
using Mono.Data.Sqlite;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    // Start is called before the first frame update


    private static string urlDataBase;
    private static IDbCommand _dbcmd;
    private static IDataReader _reader;

    private void Awake()
    {
        urlDataBase = "URI=file:" + Application.persistentDataPath + "/MiniMaze.db";
    }

    private void OnEnable()
    {
        IDbConnection _connection = new SqliteConnection(urlDataBase);
        _dbcmd = _connection.CreateCommand();
        _connection.Open();
    }

    public static void ResetDB()
    {
        var query = "DELETE FROM settings; DELETE FROM story_levels;";
        _dbcmd.CommandText = query;
        try
        {
            _dbcmd.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
            Debug.Log("Tried to reset an empty database");
        }
    }

    public static void CreateSettings(Settings settings)
    {
        var query = "CREATE TABLE IF NOT EXISTS settings (_id INT PRIMARY KEY, control_type INT,quality INT);";
        _dbcmd.CommandText = query;
        _dbcmd.ExecuteNonQuery();

        query = string.Format(
            "DELETE FROM settings;INSERT INTO settings (_id, control_type, quality) VALUES (0, {0}, {1})",
            ((int) settings.controlType).ToString(), (settings.qualitySetting).ToString());

        _dbcmd.CommandText = query;
        _dbcmd.ExecuteNonQuery();
    }

    public static Settings GetSettings()
    {
        var settings = new Settings();
        var query = "SELECT control_type, quality FROM settings WHERE _id IS 0;";
        _dbcmd.CommandText = query;
        try
        {
            _reader = _dbcmd.ExecuteReader();
        }
        catch (SqliteException e)
        {
            return null;
        }

        while (_reader.Read())
        {
            settings.controlType = (Settings.ControlType) _reader.GetInt32(0);
            settings.qualitySetting = _reader.GetInt32(1);
        }

        _reader.Close();
        return settings;
    }

    public static void UpdateSettings(Settings settings)
    {
        var query = string.Format(
            "INSERT INTO settings (_id, control_type, quality) VALUES (0, {0}, {1}) ON DUPLICATE KEY UPDATE control_type=VALUES(control_type) quality=VALUES(quality);",
            (int) settings.controlType, settings.qualitySetting);

        _dbcmd.CommandText = query;
        _dbcmd.ExecuteNonQuery();
    }

    public static bool StoryLevelsExist()
    {
        var query = "SELECT COUNT(_id) FROM story_levels";
        _dbcmd.CommandText = query;
        _reader = _dbcmd.ExecuteReader();

        int num = 0;
        while (_reader.Read())
        {
            num = _reader.GetInt32(0);
        }

        _reader.Close();
        return num > 0;
    }

    public static void CreateStoryLevels(List<LevelInfo> levels)
    {
        var query =
            "CREATE TABLE IF NOT EXISTS story_levels (_id INT PRIMARY KEY, level_name TEXT , seed TEXT , width INT, height INT, best_time FLOAT, stars INT);";
        _dbcmd.CommandText = query;
        _dbcmd.ExecuteNonQuery();

        foreach (var level in levels)
        {
            query =
                string.Format(
                    "INSERT INTO story_levels (_id, level_name, seed, width, height, best_time, stars) VALUES ({0}, '{1}', '{2}', {3}, {4}, {5}, {6})",
                    level.id.ToString(), level.levelName, level.seed, level.width.ToString(), level.height.ToString(),
                    level.time.ToString("G9"),
                    ((int) level.stars).ToString());
            _dbcmd.CommandText = query;
            _dbcmd.ExecuteNonQuery();
        }
    }

    public static void UpdateStoryLevel(LevelInfo level)
    {
        var query =
            string.Format(
                "UPDATE story_levels SET level_name = '{1}', seed = '{2}', width = {3}, height = {4}, best_time = {5}, stars = {6} WHERE _id = {0};",
                level.id, level.levelName, level.seed, level.width, level.height, level.time, (int) level.stars);
        _dbcmd.CommandText = query;
        _dbcmd.ExecuteNonQuery();
    }

    public static List<LevelInfo> GetStoryLevels()
    {
        var levels = new List<LevelInfo>();
        var query = "SELECT _id, level_name, seed, width, height, best_time, stars FROM story_levels;";
        _dbcmd.CommandText = query;
        try
        {
            _reader = _dbcmd.ExecuteReader();
        }
        catch (SqliteException e)
        {
            return null;
        }

        while (_reader.Read())
        {
            var level = new LevelInfo();
            level.id = _reader.GetInt32(0);
            level.levelName = _reader.GetString(1);
            level.seed = _reader.GetString(2);
            level.width = _reader.GetInt32(3);
            level.height = _reader.GetInt32(4);
            level.time = _reader.GetFloat(5);
            level.stars = (LevelInfo.StarsCount) _reader.GetInt32(6);

            levels.Add(level);
        }

        _reader.Close();
        levels.Sort();

        return levels;
    }

    private void OnDisable()
    {
        _dbcmd.Connection.Close();
    }
}