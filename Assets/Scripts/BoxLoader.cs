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
                        Instantiate(prefab, new Vector3(0, 3, 0), Quaternion.identity);
                   
                       BoxInformation boxInformation = prefab.GetComponent<BoxInformation>();
                       boxInformation.TextureLoader(boxFile.colour);
                    }                
                }
        }
    //void TextureLoader(string colour,GameObject prefab)
    //{
    //    string filePath = Path.Combine(Application.streamingAssetsPath,"Texture/" + colour +".png");

    //    if (File.Exists(filePath))
    //    {
    //        byte[] imageByte = File.ReadAllBytes(filePath);

    //        Texture2D texture = new Texture2D(2, 2);
    //        texture.LoadImage(imageByte);

    //        prefab.GetComponent<Renderer>().material.mainTexture = texture;
    //    }
    //    else
    //    {
    //        Debug.LogError("texture file not fount at path: " + filePath);
    //    }
    //}
}
        

