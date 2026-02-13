using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

public class BoxInformation: MonoBehaviour
{
    [SerializeField]  public string boxColour;

    private void Start()
    {
        Debug.Log("start information");
        Renderer renderer = GetComponent<Renderer>();
    }
    public void LoadBoxAsset()
    {

        Debug.Log(boxColour);
        //load textures
        string filePath = Path.Combine(Application.streamingAssetsPath,"Texture/" + boxColour + ".png");
        Debug.Log(filePath);
        if (File.Exists(filePath))
       {
            byte[] imageByte = File.ReadAllBytes(filePath);

                  Texture2D texture = new Texture2D(2, 2);
                  texture.LoadImage(imageByte);

                  GetComponent<Renderer>().material.mainTexture = texture;
              }
              else
              {
                  Debug.LogError("texture file not fount at path: " + filePath);
              }

    }
}
