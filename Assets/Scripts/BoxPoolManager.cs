using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;
using System.IO.Compression;


public class BoxPoolManager : MonoBehaviour
{
    #region Singleton
    public static BoxPoolManager Instance;

    private void Awake()
    {

        //check if instance = null
        //if not null destory gameobject.
        //return imediately

        Instance = this;
    }
    #endregion 

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public Storage package = new Storage();



    public GameObject boxPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(CreatePool());
    }

    public IEnumerator CreatePool()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "StorageData.json");
        string jsonData = File.ReadAllText(filePath);
        package = JsonUtility.FromJson<Storage>(jsonData);

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Parcels item in package.itemsToDeliver)
        {

            Queue<GameObject> objectPool = new Queue<GameObject>();

            //boxPrefab = Resources.Load<GameObject>("Prefabs/" + item.boxName);

            //loading them async helps load them more efficiently
            var request = Resources.LoadAsync<GameObject>("Prefabs/" + item.boxName);
            boxPrefab = request.asset as GameObject;

            if (boxPrefab == null)
            {
                AssetBundles bundles = GameObject.Find("assetBundle").GetComponent<AssetBundles>();
                Debug.Log("try to create: " + item.boxName);
                boxPrefab = bundles.boxPrefabs.SingleOrDefault(p => p.name == item.boxName);
            }

            for (int i = 0; i < item.poolSize; i++)
            {

                GameObject box = Instantiate(boxPrefab, new Vector3(0, 2.5f, 0), Quaternion.identity);
                box.name.Replace("(Clone)", "");

                AsyncTextureLoad loadTexture = box.GetComponent<AsyncTextureLoad>();
                yield return StartCoroutine(loadTexture.FilePath(item.boxColor));

                box.SetActive(false);
                objectPool.Enqueue(box);
            }
            poolDictionary.Add(item.boxName, objectPool);
        }

    }
    public GameObject SpawnFromPool(string itemID, Vector3 position, Quaternion rotation)
    {// this is a factory pattern
        if (!poolDictionary.ContainsKey(itemID))
        {
            Debug.LogWarning("pool with name" + itemID + " doesnt exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[itemID].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        {
            if (pooledObject != null)
            {
                pooledObject.OnObjectSpawn();
            }
        }

        poolDictionary[itemID].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
//enqueue == adds new element to queue
//dequeue == removes an element from the queue

//objectPool from Brackeys video on object pool reworked to work with loading information from Json file