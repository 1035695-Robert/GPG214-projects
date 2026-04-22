using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VoidScore : MonoBehaviour
{
    public Score currentScoreData = new Score();
    public string fileName = "ScoreSaveData.txt";
    public string textFileContents;
    [SerializeField] private int points;
    private string updatePoints;

    [SerializeField ]private TextMeshProUGUI scoreText;

    private int playerValue;
    private Players playerData = new Players();


    private void OnEnable()
    {
        EventManager.setPlayer += SetPlayerScore;
    }
    private void Start()
    {
        GetScoreFileContent();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            int zero = 0;
            string resetPoints = zero.ToString();
            
            DisplayScore(resetPoints);

            currentScoreData = new Score(resetPoints);
            
            WriteData(currentScoreData.ReturnScoreSaveData());
            GetScoreFileContent();
        }
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

    void SetPlayerScore(PlayerID id)
    {
        playerValue = (int)id;
        var playerSelected = playerData.Data[playerValue];
        if (playerSelected != null)
        {
            string score = playerSelected.PlayerScore.ToString();

            DisplayScore(score);

            currentScoreData = new Score(score);

            WriteData(currentScoreData.ReturnScoreSaveData());
            GetScoreFileContent();
        }
    }
    //private void OnApplicationQuit()
    //{
    //    var playerSelected = playerData.Data[playerValue];
    //    playerSelected.PlayerScore = currentScoreData
    //} find a way to update score to data player data
}
