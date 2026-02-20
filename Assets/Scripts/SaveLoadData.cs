using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Storage
{
    public List<Parcels> itemsToDeliver = new List<Parcels>();
}
[System.Serializable]
public class Parcels
{
    public string boxName;
    public string boxColor;
}


public class SaveLoadData : MonoBehaviour
{
    public Storage storage = new Storage();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveToJson();
        }
        if(Input.GetKeyUp(KeyCode.L))
        {
            LoadFromJson();
        }
    }
    public void SaveToJson()
    {
        string storageData = JsonUtility.ToJson(storage);
        string filePath = Application.streamingAssetsPath + "/StorageData.json";
        Debug.Log(filePath);
        File.WriteAllText(filePath, storageData);
    }

    public void LoadFromJson()
    {
        string filePath = Application.streamingAssetsPath + "/StorageData.json";
        string storageData = File.ReadAllText(filePath);

        storage = JsonUtility.FromJson<Storage>(storageData);

        Parcels[] parcelsArray = storage.itemsToDeliver.ToArray();
        for (int i = 0; i < parcelsArray.Length; i++)
        {
            Debug.Log("<color="+ parcelsArray[i].boxColor + ">" + parcelsArray[i].boxName);
        }
    }
}

