using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Pool;


public class BoxPoolManager : MonoBehaviour
{
    #region Singleton
    public static BoxPoolManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion 

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public Storage package = new Storage();

    
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

            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + item.boxName);
           

                for (int i = 0; i < item.poolSize; i++)
            {
                GameObject box = Instantiate(prefab, new Vector3(0, 2.5f, 0), Quaternion.identity);
                box.name.Replace("(Clone)", "");

                AsyncTextureLoad loadTexture = box.GetComponent<AsyncTextureLoad>();
                yield return StartCoroutine(loadTexture.LoadTextureFromFile(item.boxColor));

                box.SetActive(false);
                objectPool.Enqueue(box);
            }
            poolDictionary.Add(item.boxName, objectPool);
        }
    }
    public GameObject SpawnFromPool(string itemID, Vector3 position, Quaternion rotation)
    {
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