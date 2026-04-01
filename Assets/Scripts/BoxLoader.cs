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
    BoxPoolManager boxPool;
    public float waitTime = 1f;
    bool isWaiting = false;

    RaycastHit hit;
    float maxDistance = 2f;

    private void Start()
    {
        boxPool = BoxPoolManager.Instance;
   
    }

    public void OnEnable()
    {
        EventManager.BoxDetection.AddListener(BoxDetected);
    }
    public void OnDisable()
    {
        EventManager.BoxDetection.AddListener(BoxDetected);
    }

    public void BoxDetected()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red);
            if (hit.transform.tag != "BoxItem")
            {
                Debug.Log("no boxes found");
                SpawnBoxObjectFromPool();
            }
            
        }
        else
            {
          
                EventManager.BoxMove.Invoke();
            }
        
    }



    //void FixedUpdate()
    //{
    //    if (isWaiting == false)
    //    {
    //        StartCoroutine(SpawnBoxObjectFromPool());
    //    }
    //}


    IEnumerator SpawnBoxObjectFromPool()
    {
        //isWaiting = true;
        //yield return new WaitForSeconds(waitTime);

        Parcels[] parcelsArray = boxPool.package.itemsToDeliver.ToArray();
        //Debug.Log(parcelsArray.Length);
        int randomIndex = Random.Range(0, parcelsArray.Length);
        //Debug.Log(randomIndex);

        string itemID = parcelsArray[randomIndex].boxName;
        //Debug.Log(itemID);
        boxPool.SpawnFromPool(itemID, new Vector3(transform.position.x + 2, 1.5f, transform.position.z), Quaternion.identity);

        //isWaiting = false;
    }
}
//old Loading
//public Storage package = new Storage();

////if box is on top compare to spawn same objects

//void Start()
//{
//    StartCoroutine(LoadObjectsFromJson());
//}

//IEnumerator LoadObjectsFromJson()
//{
//    string filePath = Path.Combine(Application.streamingAssetsPath, "StorageData.json");
//    string jsonData = File.ReadAllText(filePath);
//    package = JsonUtility.FromJson<Storage>(jsonData);

//    foreach (Parcels item in package.itemsToDeliver)
//    {
//        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + item.boxName);
//        GameObject boxPrefab = Instantiate(prefab, new Vector3(0, 1.5f, 0), Quaternion.identity);

//        yield return new WaitForSeconds(waitTime);
//    }
//    yield return null;

//    //need to find a way to make the boxes load not at same time could do that with async from week3, but will need to discuss this with the teacher.
//}



