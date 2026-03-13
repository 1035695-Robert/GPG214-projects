using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;
using NUnit.Framework.Internal.Filters;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;

public class Box : MonoBehaviour, IPooledObject
{
    [Header("BOX INFO")]
    [SerializeField] private string boxName;
    [SerializeField] private string boxColor;
    [SerializeField] private int poolSize;
    [SerializeField] public int point;

    [SerializeField] private Storage package = new Storage();

    bool found = false;

    //infromation for the box here?
    public void Start()
    {
        boxName = gameObject.name.TrimEnd("(Clone)");
        transform.name = boxName;
        CheckForObjectName();
    }
    void CheckForObjectName()
    {
        Debug.Log("on activation");

        string filePath = Path.Combine(Application.streamingAssetsPath, "StorageData.JSON");

        string jsonData = File.ReadAllText(filePath);

        package = JsonUtility.FromJson<Storage>(jsonData);

        foreach (Parcels item in package.itemsToDeliver)
        {
            Debug.Log(item.boxName);
            if (boxName == item.boxName)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            GameObject target = GameObject.Find("Storage");
            JsonFileManager jsonFileManager =  target.GetComponent<JsonFileManager>();
            jsonFileManager.AddBoxToList(this.name, this.boxColor, poolSize);
        } 
        package.itemsToDeliver.RemoveAll(Parcels => Parcels.boxName != boxName);
        OnObjectSpawn();
    }
    public void OnObjectSpawn()
    {

        //HOW DO I GET THE INFROMATION FROM THE PARCEL LIST 
        for (int i = 0; i < package.itemsToDeliver.Count; i++)
        {
            string itemName = package.itemsToDeliver[i].boxName;
        }
    }
}
