using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

public class BoxInformation : MonoBehaviour
{
    public string boxColour;
    public Texture2D boxTexture;

   
    public void TextureLoader(string colour, GameObject prefab)
    {
        Debug.Log(colour);
        boxColour = Path.Combine(Application.streamingAssetsPath, "Texture/" + colour + ".png");
        
        Debug.Log(boxColour);
        LoadTextures(prefab);

    }

    private void LoadTextures(GameObject boxPrefab)
    {
        byte[] imageBytes = File.ReadAllBytes(boxColour);

       //create a temporary texture to hold our texture in 
        Texture2D boxTexture = new Texture2D(2, 2);
        //takes the byte in and convert it into an image 
        boxTexture.LoadImage(imageBytes);

        boxPrefab.GetComponent<Renderer>().material.mainTexture = boxTexture;
    }
}
