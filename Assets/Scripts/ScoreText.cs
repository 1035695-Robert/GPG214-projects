using NUnit.Framework.Internal.Filters;
using System.IO;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
     public Score currentScoreData = new Score();

     public string fileName = "ScoreSaveData.txt";

    public string textFileContents;
    private void Start()
    {
        currentScoreData = new Score("20");
        WriteData(currentScoreData.ReturnScoreSaveData());

       GetScoreFileContent();
    }

    void GetScoreFileContent()
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            if (File.Exists(filePath))
            {
                textFileContents = File.ReadAllText(filePath);
            }
            else
            {
                Debug.Log("file does not exist");
            }
        }
    }
            
            void WriteData(string dataToWrite)
     {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        Debug.Log(filePath);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(dataToWrite);
            }
        }
}
