using System.IO;
using UnityEngine;

public class BlockLoader : MonoBehaviour
{
    [System.Serializable]
    public class MyDataObject
    {
        public int id;
        public string name;
        public float value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MyDataObject dataObject = new MyDataObject();
        dataObject.id = 1;
        dataObject.name = "example";
        dataObject.value = 3.14f; 

        //convert the data object to json format
        string jsonData = JsonUtility.ToJson(dataObject);

        //define the file path to same the json file
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");
       

        try
        {
            //write the json data to the file
            File.WriteAllText(filePath, jsonData);
            Debug.Log("json data successfully writen to file " + filePath);
        }
        catch(System.Exception e)
        {
            Debug.Log("failed to write json data to file " + e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
