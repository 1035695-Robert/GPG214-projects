using System;
using UnityEngine;

[Serializable]
public class Score
{
    [SerializeField] private int score;

    //default
    public Score()
    {

    }
    public Score(string scoreData)
    {
        if (!int.TryParse(scoreData, out score))
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





