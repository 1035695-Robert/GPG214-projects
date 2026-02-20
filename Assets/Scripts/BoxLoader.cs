using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class BoxLoader : MonoBehaviour
{
    public Storage package = new Storage();

    public float waitTime = 1f;

    //if box is on top compare to spawn same objects

    void Start()
    {
        StartCoroutine(LoadObjectsFromJson());
    }

    IEnumerator LoadObjectsFromJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "StorageData.json");
        string jsonData = File.ReadAllText(filePath);
        package = JsonUtility.FromJson<Storage>(jsonData);

        foreach (Parcels item in package.itemsToDeliver)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + item.boxName);
            GameObject boxPrefab = Instantiate(prefab, new Vector3(0, 1.5f, 0), Quaternion.identity);

            yield return new WaitForSeconds(waitTime);
        }
        yield return null;

        //need to find a way to make the boxes load not at same time could do that with async from week3, but will need to discuss this with the teacher.
    }
}

        
