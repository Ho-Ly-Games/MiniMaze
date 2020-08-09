using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security;
using Game;
using Mono.Data.Sqlite;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseHandler : MonoBehaviour
{
    // Start is called before the first frame update


    private static string urlDataBase;
    private static IDbCommand dbcmd;
    private static IDataReader reader;

    private void Awake()
    {
        urlDataBase = "URI=file:" + Application.persistentDataPath + "MiniMaze.db";
    }

    private void OnEnable()
    {
        IDbConnection _connection = new SqliteConnection(urlDataBase);
        dbcmd = _connection.CreateCommand();
        _connection.Open();
    }

    public void ResetDB()
    {
        var query = "DELETE FROM settings; DELETE FROM story_levels;";
        dbcmd.CommandText = query;
        dbcmd.ExecuteNonQuery();
    }

    public void CreateSettings(Settings settings)
    {
        var query = "CREATE TABLE IF NOT EXISTS settings (_id INT PRIMARY KEY, control_type INT,quality INT);";
        dbcmd.CommandText = query;
        dbcmd.ExecuteNonQuery();

        query = string.Format(
            "INSERT INTO settings (_id, control_type, quality) VALUES (0, {0}, {1}) ON DUPLICATE KEY UPDATE control_type=VALUES(control_type) quality=VALUES(quality);",
            (int) settings.controlType, settings.qualitySetting);

        dbcmd.CommandText = query;
        dbcmd.ExecuteNonQuery();
    }

    public Settings GetSettings()
    {
        var settings = new Settings();
        var query = "SELECT control_type, quality FROM settings WHERE _id IS 0;";
        dbcmd.CommandText = query;
        var reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            settings.controlType = (Settings.ControlType) reader.GetInt32(0);
            settings.qualitySetting = reader.GetInt32(1);
        }

        return settings;
    }

    public void UpdateSettings(Settings settings)
    {
        var query = string.Format(
            "INSERT INTO settings (_id, control_type, quality) VALUES (0, {0}, {1}) ON DUPLICATE KEY UPDATE control_type=VALUES(control_type) quality=VALUES(quality);",
            (int) settings.controlType, settings.qualitySetting);

        dbcmd.CommandText = query;
        dbcmd.ExecuteNonQuery();
    }

    public bool StoryLevelsExist()
    {
        var query = "SELECT COUNT(_id) FROM story_levels";
        dbcmd.CommandText = query;
        var reader = dbcmd.ExecuteReader();

        int num = 0;
        while (reader.Read())
        {
            num = reader.GetInt32(0);
        }

        return num > 0;
    }

    public void CreateStoryLevels(List<LevelInfo> levels)
    {
    }

    public void UpdateStoryLevel(LevelInfo level)
    {
    }

    public List<LevelInfo> GetStoryLevels()
    {
    }


    [Button]
    public static void CreateTable(string tableName)
    {
        string sql =
            $"CREATE TABLE IF NOT EXISTS {tableName} (_id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR (20), score INT);";
        dbcmd.CommandText = sql;

        dbcmd.ExecuteNonQuery();
    }

    [Button]
    public static void InsertInto(string tableName, string name, int score)
    {
        string sql = string.Format("INSERT INTO {0} (name, score) VALUES ('{1}', {2})", tableName, name, score);
        dbcmd.CommandText = sql;
        dbcmd.ExecuteNonQuery();
    }

    [Button]
    public static void Delete(string tableName, string name)
    {
        string sql = string.Format("DELETE FROM {0} WHERE name='{1}'", tableName, name);
        dbcmd.CommandText = sql;
        dbcmd.ExecuteNonQuery();
    }

    [Button]
    public static void DropTable(string tableName)
    {
        string sql = string.Format("DROP TABLE {0}", tableName);
        dbcmd.CommandText = sql;
        dbcmd.ExecuteNonQuery();
    }

    [Button]
    public static void UpdateWhere(string tableName, string name, int score)
    {
        string sql = string.Format("UPDATE {0} SET score={1} WHERE name='{1}'", tableName, score, name);
        dbcmd.CommandText = sql;
        dbcmd.ExecuteNonQuery();
    }

    private void OnDisable()
    {
        dbcmd.Connection.Close();
    }
}