using System.IO;
using UnityEditor;
using UnityEngine;

public class DataAnalytics : MonoBehaviour
{
    public PlayerID playerID;
    public string filePath;
    private string textFile = "DataAnalytics.txt";
    public float selectionTime;
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
        playerID = ID;
        selectionTime = Time.time;
    }


    private void OnApplicationQuit()
    {
        if (playerID != PlayerID.None)
        {
            float playDuration = Time.time;
            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(
                playerID + 
                "\n\ttime taken to select Player " + selectionTime.ToString("F2") +
                "\n\tPlayed for " + (playDuration - selectionTime).ToString("F2") + " seconds after Selection"
                );
            sw.Flush();
            sw.Close();
        }
    }

}
