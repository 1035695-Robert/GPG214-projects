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


    public BoxList myBoxList = new BoxList();

    public float waitTime = 1f;


    void Start()
    {
        StartCoroutine(LoadObjectsFromJson());
    }

    IEnumerator LoadObjectsFromJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "BoxList.JSON");

        string jsonData = File.ReadAllText(filePath);

        myBoxList = JsonUtility.FromJson<BoxList>(jsonData);
        foreach (Box boxFile in myBoxList.box)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + boxFile.name);
           GameObject boxPrefab = Instantiate(prefab, new Vector3(0, 1.5f, 0), Quaternion.identity);
            
            BoxInformation boxInformation = prefab.GetComponent<BoxInformation>();
            boxInformation.TextureLoader(boxFile.colour, boxPrefab); 

            yield return new WaitForSeconds(waitTime);
        }
        yield return null;


        //need to find a way to make the boxes load not at same time could do that with async from week3, but will need to discuss this with the teacher. 
    }
}

        
