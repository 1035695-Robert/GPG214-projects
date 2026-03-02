using System;
using System.IO;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Score
{
    [SerializeField] private float score;

    //default
    public Score()
    {

    }
    public Score(string scoreData)
    {
        if (!float.TryParse(scoreData, out score))
        {
            Debug.LogError("CANNOT convert score to integer");
        }
    }
    public string ReturnScoreSaveData()
    {
        string returnData = "Score: " + score;
        return returnData;
    }
}





