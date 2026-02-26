using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class BoxPoolManager : MonoBehaviour
{
    public static BoxPoolManager instance;

    public GameObject boxObjects;
    public int poolSize = 30;

    public List<GameObject> boxPool;
    public Storage storage = new Storage();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        InitializeBoxPool();
       
    }
    public void InitializeBoxPool()
    {
        boxPool = new List<GameObject>();
       
        for (int i = 0; i < poolSize; i++)
        {
            GameObject box = Instantiate(boxObjects);
            box.SetActive(false);
            boxPool.Add(box);
        }
    }
    
    public GameObject GetPooledBox()
    {
        foreach (var box in boxPool)
        {
            if (!box.activeInHierarchy)
            {
                return box;
            }
        }

        GameObject newBox = Instantiate(boxObjects);
        newBox.SetActive(true);
        boxPool.Add(newBox);

        return newBox;
    }
}
