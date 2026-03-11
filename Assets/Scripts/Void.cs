using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Void : MonoBehaviour
{
    public Score currentScoreData = new Score();
    public string fileName = "ScoreSaveData.txt";
    public string textFileContents;
    [SerializeField] private int points;
    private string updatePoints;

    [SerializeField ]private TextMeshProUGUI scoreText;
   
    private void Start()
    {
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
                Debug.Log(textFileContents);
                string pointString = textFileContents.TrimStart("Score: ");
                Debug.Log(pointString);
                if (!int.TryParse(pointString, out points))
                {
                    Debug.LogError("couldnt get points");
                }
                scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
                DisplayScore(pointString);
            }
            else
            {
                Debug.LogWarning("file does not exist");
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "BoxItem")
        {
            Box boxScript = collision.transform.GetComponent<Box>();
            points += boxScript.point;
            updatePoints = points.ToString();
            collision.gameObject.SetActive(false);

            DisplayScore(updatePoints);

            currentScoreData = new Score(updatePoints);
            WriteData(currentScoreData.ReturnScoreSaveData());
        }
    }
    private void DisplayScore(string points)
    { 
       scoreText.text = "Box Score: " + points;
    }
}
