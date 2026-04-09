
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;


public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private Texture2D levelTexture;
    [SerializeField] private List<GameObject> objectToSpawn = new List<GameObject>();
    [SerializeField] private int cellSize;
    bool isComplete;

    private void Awake()
    {
        GameObject[] loadObjects = Resources.LoadAll<GameObject>("Prefabs/LevelAssets");
        objectToSpawn = loadObjects.ToList();
    }
    void OnEnable()
    {
        EventManager.generateLevel += GenerateLevel;
    }
    IEnumerator GenerateLevel()
    {

        for (int x = 0; x < levelTexture.height; x++)
        {
            for (int z = 0; z < levelTexture.width; z++)
            {
                Debug.Log(x + ", " + z);
                Color color = levelTexture.GetPixel(x, z);
               // Debug.Log(color);
                string hexColor = color.ToHexString();
                //Debug.Log(hexColor);
                Vector3 spawnPosition = GridPlacement(x, z);

                switch (hexColor)
                {
                    case "FFFFFFFF": // White = Null/Default 
                        break;

                    case "00FF00FF": // green = BoxLoaders
                        Debug.Log("GREEN");
                        GameObject loader = objectToSpawn[0];
                        spawnPosition.y = 2f;
                        ItemSpawn(spawnPosition, loader);

                        break;

                    case "0000FFFF": //Blue = Belt
                        Debug.Log("BLUE " + spawnPosition);
                        GameObject belt = objectToSpawn[1];
                        ItemSpawn(spawnPosition, belt);
                        break;

                    case "FF0000FF": //Red = Goals
                        //Debug.Log("RED");
                        GameObject goal = objectToSpawn[2];
                        ItemSpawn(spawnPosition, goal);
                        break;

                    case "00000000": //black = Obstacles
                        GameObject obstacle = objectToSpawn[3];
                        ItemSpawn(spawnPosition, obstacle);
                        break;
                }
            }
        }
        yield return null;
    }

    GameObject ItemSpawn(Vector3 spawnPosition, GameObject prefab)
    {
        return Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GridPlacement(int X, int Z)
    {
        float x = Mathf.RoundToInt(X) * cellSize;
        float z = Mathf.RoundToInt(Z) * cellSize;

        return new Vector3(x, 0.5f, z);
    }
}
