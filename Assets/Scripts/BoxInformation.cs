using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;
using NUnit.Framework.Internal.Filters;

public class BoxInformation : MonoBehaviour
{
    [Header ("BOX INFO")]
    [SerializeField] private string boxName;
    [SerializeField] private string boxColor;

    [SerializeField] private Storage package = new Storage();

    bool found = false;

    //infromation for the box here?
    public void Start()
    {
        boxName = gameObject.name.TrimEnd("(Clone)");
        CheckForObjectName();
    }
    void CheckForObjectName()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "StorageData.JSON");

        string jsonData = File.ReadAllText(filePath);

        package = JsonUtility.FromJson<Storage>(jsonData);

        foreach (Parcels item in package.itemsToDeliver)        
        {
            Debug.Log(item.boxName);
            if (boxName == item.boxName)
            {
                boxColor = item.boxColor;
                found = true;
                break;
            } 
        }
        if (!found)
        {
            Debug.LogError("error could not find" + boxName);
        }
        package.itemsToDeliver.RemoveAll(Parcels => Parcels.boxName != boxName);
    }
}
