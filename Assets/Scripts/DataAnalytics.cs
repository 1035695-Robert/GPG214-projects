using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DataAnalytics : MonoBehaviour
{
    public string playerID;
    public List<string> playerList = new List<string>();
    public string textFilePath;
    public string filePath;

    public Players playerInfo = new Players();
    private string textFile = "DataAnalytics.txt";

    private int playerIDvalue;
    public float selectionTime;

    private LevelID levelIDValue;



    string horizontalLine = new string("'-',10");
    private void Start()
    {
        filePath = Application.streamingAssetsPath + "/PlayerData.json";
        textFilePath = Path.Combine(Application.streamingAssetsPath, textFile);
        DontDestroyOnLoad(this.gameObject);
    }
    public void OnEnable()
    {
        EventManager.setPlayer += SetPlayerID;
        EventManager.setLevel += SetLevelID;
    }
    public void OnDisable()
    {
        EventManager.setPlayer += SetPlayerID;
        EventManager.setLevel -= SetLevelID;
    }
    void SetPlayerID(PlayerID ID)
    {

        playerIDvalue = (int)ID;
        playerID = ID.ToString();
        Debug.Log(playerID);
        Debug.Log(playerIDvalue + " " + playerID);

        string playerRead = File.ReadAllText(filePath);
        playerInfo = JsonUtility.FromJson<Players>(playerRead);
         var selectedPlayer = playerInfo.Data[(int)ID];
        Debug.Log(selectedPlayer.PlayerScore + selectedPlayer.hasDLC.ToString());


        selectionTime = Time.time;
    }

    public void SetLevelID( LevelID levelId)
    {
        levelIDValue = levelId; 
    }


    private void OnApplicationQuit()
    {
        if (playerID != null)
        {

            float playDuration = Time.time;
            var selectedPlayer = playerInfo.Data[playerIDvalue];
            StreamWriter sw = File.AppendText(textFilePath);
           
                // appendText adds new line of text in file.
            sw.WriteLine(                                   // each time game ends it will log a new block of text based on GamePlay
                new string('-', 10) +
                "\n"   + playerID + ":" +
                "\n" + DateTime.Now.ToString("MMMM dd, yyyy") +
                "\n\tCurrent Score: " + selectedPlayer.PlayerScore.ToString() +
                "\n\tDLC:" + selectedPlayer.hasDLC.ToString() +
                "\n\tLevel Selected: " + levelIDValue + 
                "\n\tTime to Select Player " + selectionTime.ToString("F2") +
                "\n\tPlayed for " + (playDuration - selectionTime).ToString("F2") + " seconds after Selection"
                );
 
            sw.Flush();
            sw.Close();
            PlayerCount();
        }


    }
    public void PlayerCount()
    {
        string[] lineToEdit = File.ReadAllLines(textFilePath);

        int count = File.ReadLines(textFilePath).Count(line => line.Contains(playerID));

        switch (playerID)
        {
            case "player1":
                lineToEdit[1] = "\tPlayer1: " + count;
                File.WriteAllLines(textFilePath, lineToEdit);
                Debug.Log("");
                break;
            case "player2":
                lineToEdit[2] = "\tPlayer2: " + count;
                File.WriteAllLines(textFilePath, lineToEdit);
                break;
            case "player3":
                lineToEdit[3] = "\tPlayer3: " + count;
                File.WriteAllLines(textFilePath, lineToEdit);
                break;

        }
    }

}