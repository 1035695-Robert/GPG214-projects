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

    RaycastHit hit;
    RaycastHit[] hits = new RaycastHit[10];
    float maxDistance = 4f;


    private void Start()
    {
        boxPool = BoxPoolManager.Instance;
    }

    public void OnEnable()
    {
        EventManager.boxDetection += BoxDetected;
    }
    public void OnDisable()
    {
        EventManager.boxDetection -= BoxDetected;
    }

    public void BoxDetected()
    {
        //int count = Physics.RaycastNonAlloc(transform.position, transform.forward, hits, maxDistance)



        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red);
            if (hit.transform.tag == "BoxItem")
            {
                EventManager.boxMove.Invoke();
            }
        }
        else
        {
            Debug.Log("no boxes found");
            StartCoroutine(SpawnBoxObjectFromPool());

        }
    }

    public IEnumerator SpawnBoxObjectFromPool()
    {
        //isWaiting = true;
        // yield return new WaitForSeconds(waitTime);
        
        Parcels[] parcelsArray = boxPool.package.itemsToDeliver.ToArray();
        //Debug.Log(parcelsArray.Length);
        int randomIndex = Random.Range(0, parcelsArray.Length);
        //Debug.Log(randomIndex);

        string itemID = parcelsArray[randomIndex].boxName;
        //Debug.Log(itemID);
        if (itemID != null)
        {
            boxPool.SpawnFromPool(itemID, new Vector3(transform.localPosition.x + 2, transform.position.y + 2,transform.position.z) , Quaternion.identity);
        }
        yield return null;
        // isWaiting = false;
    }
}


