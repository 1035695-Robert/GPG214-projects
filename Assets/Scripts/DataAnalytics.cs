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
    public string filePath;
    private string textFile = "DataAnalytics.txt";
    public float selectionTime;
    public bool isdead;


    string horizontalLine = new string("'-',10");
    private void Start()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, textFile);
    }
    public void OnEnable()
    {
        EventManager.setPlayer += SetPlayerID;
    }
    public void OnDisable()
    {
        EventManager.setPlayer += SetPlayerID;

    }
    void SetPlayerID(PlayerID ID)
    {

        playerID = ID.ToString();

        selectionTime = Time.time;


    }


    private void OnApplicationQuit()
    {
        if (playerID != null)
        {

            float playDuration = Time.time;
            StreamWriter sw = File.AppendText(filePath);     // appendText adds new line of text in file.
            sw.WriteLine(                                   // each time game ends it will log a new block of text based on GamePlay
                new string('-', 10) +
                "\n" + playerID + ": "
                + "\n" + DateTime.Now.ToString("MMMM dd, yyyy") +
                "\n\ttime taken to select Player " + selectionTime.ToString("F2") +
                "\n\tPlayed for " + (playDuration - selectionTime).ToString("F2") + " seconds after Selection"
                //add Score
                );



            sw.Flush();
            sw.Close();
            PlayerCount();
        }


    }
    public void PlayerCount()
    {
        string[] lineToEdit = File.ReadAllLines(filePath);

        int count = File.ReadLines(filePath).Count(line => line.Contains(playerID));

        switch (playerID)
        {
            case "player1":
                lineToEdit[1] = "\tPlayer1: " + count;
                File.WriteAllLines(filePath, lineToEdit);
                Debug.Log("");
                break;
            case "player2":
                lineToEdit[2] = "\tPlayer1: " + count;
                File.WriteAllLines(filePath, lineToEdit);
                break;
            case "player3":
                lineToEdit[3] = "\tPlayer1: " + count;
                File.WriteAllLines(filePath, lineToEdit);
                break;

        }
    }

}