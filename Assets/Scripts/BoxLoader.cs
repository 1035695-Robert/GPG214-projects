using JetBrains.Annotations;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class BoxLoader : MonoBehaviour
{


    public BoxList myBoxList = new BoxList();
    
    void Start()
    {   
        LoadObjectsFromJson();
    }

        void LoadObjectsFromJson()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "BoxList.JSON");
          
            string jsonData = File.ReadAllText(filePath);

                myBoxList = JsonUtility.FromJson<BoxList>(jsonData);

                foreach(Box boxFile in myBoxList.box)
                {
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/" + boxFile.name);
                    if (prefab != null)
                    {
                        GameObject boxPrefab = Instantiate(prefab, new Vector3(0, 3, 0), Quaternion.identity);
                   
                       BoxInformation boxInformation = prefab.GetComponent<BoxInformation>();
                       boxInformation.TextureLoader(boxFile.colour, boxPrefab);
                    }                
                }
                //need to find a way to make the boxes load not at same time could do that with async from week3, but will need to discuss this with the teacher. 
        }
}
        

