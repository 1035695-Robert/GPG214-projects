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

    private int IDvalue;
    public float selectionTime;



    string horizontalLine = new string("'-',10");
    private void Start()
    {
        filePath = Application.streamingAssetsPath + "/PlayerData.json";
        textFilePath = Path.Combine(Application.streamingAssetsPath, textFile);
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

        IDvalue = (int)ID;
        playerID = ID.ToString();
        Debug.Log(playerID);
        Debug.Log(IDvalue + " " + playerID);

        string playerRead = File.ReadAllText(filePath);
        playerInfo = JsonUtility.FromJson<Players>(playerRead);
         var selectedPlayer = playerInfo.Data[(int)ID];
        Debug.Log(selectedPlayer.PlayerScore + selectedPlayer.hasDLC.ToString());


        selectionTime = Time.time;


    }


    private void OnApplicationQuit()
    {
        if (playerID != null)
        {

            float playDuration = Time.time;
            var selectedPlayer = playerInfo.Data[IDvalue];
            StreamWriter sw = File.AppendText(textFilePath);
            sw.WriteLine("<html><body>");
                // appendText adds new line of text in file.
            sw.WriteLine("<detail>");
            sw.WriteLine(                                   // each time game ends it will log a new block of text based on GamePlay
                new string('-', 10) +
                "\n "   + playerID + ":" +
                "\n" + DateTime.Now.ToString("MMMM dd, yyyy") +
                "\n\tCurrent Score: " + selectedPlayer.PlayerScore.ToString() +
                "\n\tDLC:" + selectedPlayer.hasDLC.ToString() +
                
                "\n\ttime taken to select Player " + selectionTime.ToString("F2") +
                "\n\tPlayed for " + (playDuration - selectionTime).ToString("F2") + " seconds after Selection"
                );
            sw.WriteLine("</detail>");
            sw.WriteLine("</body></html>");
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