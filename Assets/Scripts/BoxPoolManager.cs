using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;
using UnityEngine.UIElements;


public class BoxPoolManager : MonoBehaviour
{
    #region Singleton
    public static BoxPoolManager Instance;
   public AssetBundles bundle;
    private void Awake()
    {

        //check if instance = null
        //if not null destory gameobject.
        //return imediately
        Instance = this;

      

    }
    private void OnEnable()
    {
        EventManager.objectPool += CreatePool;
        EventManager.applyDlc += ApplyDlc;
    }

    private void OnDisable()
    {
        EventManager.objectPool -= CreatePool;
        EventManager.applyDlc -= ApplyDlc;
    }
    #endregion 

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public Storage package = new Storage();

    public string filePath;
    TextureLoadAsync loadTexture;

    Texture boxTexture;
    Texture l1Texture;

    public GameObject boxPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void ApplyDlc()
    {
        bundle = GameObject.Find("assetBundle").GetComponent<AssetBundles>();
    }

    public IEnumerator CreatePool()
    {
        loadTexture = GetComponent<TextureLoadAsync>();

        filePath = Path.Combine(Application.streamingAssetsPath, "StorageData.json");
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
                if (bundle == null)
                {
                    yield break;
                }

                foreach (string path in bundle.assetPath)
                {
                    Debug.Log(path);
                    if (path.Contains("box"))
                    {
                        boxPrefab = bundle.dlcBundle.LoadAsset<GameObject>(path);

                        if (boxPrefab == null)
                        {

                        }
                    }
                    if(path.Contains(item.boxColor.ToLower() + ".png"))
                    {
                        Debug.Log("Dlc texture");
                        boxTexture = bundle.dlcBundle.LoadAsset<Texture>(path);
                        l1Texture = bundle.dlcBundle.LoadAsset<Texture>("L1" + path);
                    }
                }
               
            }
            else
            {
                if (boxPrefab != null)
                {
                    yield return StartCoroutine(loadTexture.FilePath(item.boxColor));
                    boxTexture = loadTexture.ReturnTexture();
                }
            }
            for (int i = 0; i < item.poolSize; i++)
            {

                GameObject box = Instantiate(boxPrefab, new Vector3(0, 2.5f, 0), Quaternion.identity);

                box.name.TrimEnd("(Clone)");
                box.SetActive(false);

                AddTexture(box, boxTexture, item.boxColor);

                objectPool.Enqueue(box);
            }
            poolDictionary.Add(item.boxName, objectPool);
        }
        yield break;
    }

    public void AddTexture(GameObject box, Texture texture, string boxColor)
    {
        if (texture != null)
        {
            box.GetComponent<Renderer>().material.mainTexture = texture;
            box.GetComponent<Renderer>().material.color = Color.white;

            Transform L1 = box.transform.Find("LOD1");
            if (L1 != null)
            {
                if (UnityEngine.ColorUtility.TryParseHtmlString(boxColor.ToLower(), out Color newColor))
                {
                    L1.GetComponent<Renderer>().material.color = newColor;
                }
                else
                    Debug.LogError("no match");
            }
        }
        else
        {
            box.GetComponent<Renderer>().material.color = Color.magenta;
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

//objectPool from Brackeys video on object pool reworked to work with loading information from Json file and addition of loading object from assetbundles