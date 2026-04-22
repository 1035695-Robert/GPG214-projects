using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


[Serializable]
public class PlayerInfo
{
    public PlayerID Id; //will be saved as an int
    //public Enum CurrentLevel;
    public int PlayerScore;
    public bool hasDLC;
}
[Serializable]
public class Players
{
    public List<PlayerInfo> Data = new List<PlayerInfo>();
}

public class PlayerData : MonoBehaviour
{
    public PlayerID player;

    public Players playerInfo = new Players();

    public string filePath;

    private void Start()
    {
       
        // LoadSpecificPlayer();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("saving?");
            SavePlayerData();
        }
    }
    public void OnEnable()
    {
        EventManager.setPlayer += PlayerSelect;
    }
    public void OnDisable()
    {
        EventManager.setPlayer -= PlayerSelect;
    }

    void PlayerSelect(PlayerID selectedPlayer)
    {
        player = selectedPlayer;
        LoadSpecificPlayer();
    }
    public void SavePlayerData()
    {
        filePath = Application.streamingAssetsPath + "/PlayerData.json";
        using (StreamWriter sw = new StreamWriter(filePath, append: true))
        {
            foreach(var player in playerInfo.Data)
            {
                string line = JsonUtility.ToJson(player); 
                sw.WriteLine(line);
            }
        }
        
        string jsonFile = JsonUtility.ToJson(playerInfo, true);
             
        File.WriteAllText(filePath, jsonFile); 

    }
    public void LoadSpecificPlayer()
    {
        filePath = Application.streamingAssetsPath + "/playerData.json";

        if (File.Exists(filePath))
        {
            string playerFileData = File.ReadAllText(filePath);
            Debug.Log(playerFileData);

            playerInfo = JsonUtility.FromJson<Players>(playerFileData);
            ////return the first player that matches the enum.
            PlayerInfo selectedPlayer = playerInfo.Data.FirstOrDefault(i => i.Id == player);
            Debug.Log(selectedPlayer.Id + "score" + selectedPlayer.PlayerScore);

        }
        else
            Debug.LogError("missing");

        //return null;

    }
}
