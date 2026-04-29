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
   
    public int PlayerScore;
    public bool hasDLC;
}
[Serializable]
public class Players
{
    public List<PlayerInfo> Data;
}

public class PlayerData : MonoBehaviour
{
    public PlayerID player;

    public Players playerInfo = new Players();

    public string filePath;

    private void Start()
    {
        //  SavePlayerData();
       // LoadSpecificPlayer();
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
        filePath = Path.Combine(Application.streamingAssetsPath, "playerData.json");
        string jsonFile = JsonUtility.ToJson(playerInfo);

        Debug.Log(filePath);

        File.WriteAllText(filePath, jsonFile);

    }
    public void LoadSpecificPlayer()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "playerData.json");

        if (File.Exists(filePath))
        {
            string playerFileData = File.ReadAllText(filePath);
            Debug.Log(playerFileData);

            playerInfo = JsonUtility.FromJson<Players>(playerFileData);
            
            ////return the first player that matches the enum.
            PlayerInfo selectedPlayer = playerInfo.Data.FirstOrDefault(i => i.Id == player);
            EventManager.dlcCheck.Invoke(selectedPlayer.hasDLC);
            Debug.Log(selectedPlayer.Id + "score" + selectedPlayer.PlayerScore);

        }
        else
            Debug.LogError("missing path");
        //return null;

    }
}
