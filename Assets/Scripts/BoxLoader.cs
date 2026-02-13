using JetBrains.Annotations;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class BoxLoader : MonoBehaviour
{
    public TextAsset boxTextJSON;

    public BoxList myBoxList = new BoxList();



    void Start()
    {
        LoadObjectsFromJson();
    }

        void LoadObjectsFromJson()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "BoxList.JSON");
            if (!File.Exists(filePath))
            {
                string boxJson = File.ReadAllText(filePath);
            }
                string jsonData = File.ReadAllText(filePath);

                 myBoxList = JsonUtility.FromJson<BoxList>(jsonData);

               foreach(Box boxFile in myBoxList.box)
                {
                Debug.Log("box colour " + boxFile.colour);
                Debug.Log("loading " + boxFile.name);

            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + boxFile.name);
                    if (prefab != null)
                    {
                        Instantiate(prefab, new Vector3(0, 3, 0), Quaternion.identity);
                    }
                }
                
            }
        }
        

