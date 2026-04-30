
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using NUnit.Framework;



public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private List<Texture2D> levelMaps = new List<Texture2D>();
    [SerializeField] private List<GameObject> objectToSpawn = new List<GameObject>();
    [SerializeField] private int cellSize;
    bool isComplete;

    public AssetBundles bundle;
    private void Awake()
    {


    }
    private void Start()
    {


        GameObject[] loadObjects = Resources.LoadAll<GameObject>("Prefabs/LevelAssets");
        objectToSpawn = loadObjects.ToList();
        Texture2D defaultMap = Resources.Load<Texture2D>("level_1");
        levelMaps.Add(defaultMap);

        // load AssetBundle


    }

    void OnEnable()
    {
        EventManager.setLevel += StartLevelGen;
        EventManager.applyDlc += ApplyDLC;
    }
    private void OnDisable()
    {
        EventManager.setLevel -= StartLevelGen;
        EventManager.applyDlc -= ApplyDLC;
    }

    void ApplyDLC()
    {
        bundle = GameObject.Find("assetBundle").GetComponent<AssetBundles>();

        if (bundle != null)
        {

            foreach (string path in bundle.assetPath)
            {
                Debug.Log(path);
                if (path.Contains("newlevel/assets/prefab/"))
                {
                    GameObject dlcAsset = bundle.dlcBundle.LoadAsset<GameObject>(path);
                    if (dlcAsset != null)
                    {
                        objectToSpawn.Add(dlcAsset);
                    }
                }
                if (path.Contains("newlevel"))
                {
                    Texture2D dlcMap = bundle.dlcBundle.LoadAsset<Texture2D>(path);
                    if (dlcMap != null)
                    {
                        levelMaps.Add(dlcMap);
                    }
                }
            }
        }
        else
        {
            return;
        }
    }
   void StartLevelGen(LevelID levelID)
    {
        StartCoroutine(GenerateLevel(levelID));
    }
    IEnumerator GenerateLevel(LevelID level)
    {
        Texture2D selectedLevelMap = levelMaps[(int)level];
        if (selectedLevelMap == null) Debug.LogError("no map selected");
        else
            Debug.Log(selectedLevelMap);

            for (int x = 0; x < selectedLevelMap.height; x++)
            {
                for (int z = 0; z < selectedLevelMap.width; z++)
                {
                    //Debug.Log(x + ", " + z);
                    Color color = selectedLevelMap.GetPixel(x, z);
                    // Debug.Log(color);
                    string hexColor = color.ToHexString();
                    Debug.Log(hexColor);
                    Vector3 spawnPosition = GridPlacement(x, z);

                    switch (hexColor)
                    {
                        case "00FF00FF": 
                        GameObject loader = objectToSpawn.FirstOrDefault(obj => obj.name.Contains("BoxLoader"));
                            spawnPosition.y = 2f;
                            GameObject target = ItemSpawn(spawnPosition, loader);
                            target.transform.Rotate(0, 90, 0);

                            break;

                        case "0000FFFF": //Blue = Belt
                            GameObject belt = objectToSpawn.FirstOrDefault(obj => obj.name.Contains("ConveyorBelt"));
                            ItemSpawn(spawnPosition, belt);
                            break;

                        case "": //splitBelts
                            break;

                        case "000000FF": //black = Obstacles
                    
                            GameObject obstacle = objectToSpawn.FirstOrDefault(obj => obj.name.Contains("Obstacles"));
                            ItemSpawn(spawnPosition, obstacle);
                            break;

                        case "FF0000FF": //Red = Goals
                            
                            var result = objectToSpawn.FirstOrDefault(i => i.name.Contains("Bin"));
                            GameObject goal = result;
                            ItemSpawn(spawnPosition, goal);
                            break;


                    }
                }
            }
        StaticBatchingUtility.Combine(this.gameObject);
        yield return null;
    }


    GameObject ItemSpawn(Vector3 spawnPosition, GameObject prefab)
    {
        GameObject target = Instantiate(prefab, spawnPosition, Quaternion.identity);
        target.transform.parent = this.transform;
        target.name = prefab.name;
        EventManager.ItemTextureLoad.Invoke(target);

        


        return target;
    }

    Vector3 GridPlacement(int X, int Z)
    {
        float x = Mathf.RoundToInt(X) * cellSize;
        float z = Mathf.RoundToInt(Z) * cellSize;

        return new Vector3(x, 0.5f, z);
    }
}
