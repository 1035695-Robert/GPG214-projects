using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public GameObject boxObjects;
    public int poolSize = 10;

    private List<GameObject> boxPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        InitializeBoxPool();

    }
    private void InitializeBoxPool()
    {
        boxPool = new List<GameObject>();
      
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
