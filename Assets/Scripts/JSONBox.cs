using System.ComponentModel;
using System.IO;
using UnityEngine;

public class JSONBox : MonoBehaviour
{

    [System.Serializable]
    public class Box
    {
        public string name;
        public string colour;
        //add other data types later
    }
    [System.Serializable]
    public class BoxList
    {
        public Box[] box;
    }
    public BoxList myBoxList = new BoxList();
    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "BoxList.JSON");
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

                
            myBoxList = JsonUtility.FromJson<BoxList>(jsonData);

            Debug.Log("load data: " + myBoxList);
        }
        else
        {
            Debug.LogError("JSON file not found: " + filePath);
        }

    }
}

