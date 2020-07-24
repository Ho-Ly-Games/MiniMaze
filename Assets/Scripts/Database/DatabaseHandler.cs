﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        urlDataBase = "URI=file:" + Application.persistentDataPath + "MasterSQLite.db";
    }

    [Button]
    public static void Initiate () {
        IDbConnection _connection = new SqliteConnection (urlDataBase);
        dbcmd = _connection.CreateCommand ();
        _connection.Open ();
    }

    [Button]
    public static void CreateTable (string tableName) {
        string sql = string.Format ("CREATE TABLE IF NOT EXISTS {0} (_id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR (20), score INT);", tableName);
        dbcmd.CommandText = sql;

        dbcmd.ExecuteNonQuery ();
    }

    [Button]
    public static void InsertInto (string tableName, string name, int score) {
        string sql = string.Format ("INSERT INTO {0} (name, score) VALUES ('{1}', {2})", tableName, name, score);
        dbcmd.CommandText = sql;
        dbcmd.ExecuteNonQuery ();
    }

    [Button]
    public static void Delete (string tableName, string name) {
        string sql = string.Format ("DELETE FROM {0} WHERE name='{1}'", tableName, name);
        dbcmd.CommandText = sql;
        dbcmd.ExecuteNonQuery ();
    }

    [Button]
    public static void DropTable (string tableName) {
        string sql = string.Format ("DROP TABLE {0}", tableName);
        dbcmd.CommandText = sql;
        dbcmd.ExecuteNonQuery ();
    }

    [Button]
    public static void UpdateWhere (string tableName, string name, int score) {
        string sql = string.Format ("UPDATE {0} SET score={1} WHERE name='{1}'", tableName, score, name);
        dbcmd.CommandText = sql;
        dbcmd.ExecuteNonQuery ();
    }
    
    
    private void OnEnable()
    {
        //connect to database
    }

    
    

    private void OnDisable()
    {
        //disconnect from database
    }
}