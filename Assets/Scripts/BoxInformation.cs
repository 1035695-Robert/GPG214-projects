using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

public class BoxInformation : MonoBehaviour
{
    public string boxColour;

    public void TextureLoader(string colour)
    {
        Debug.Log(colour);
        boxColour = Path.Combine(Application.streamingAssetsPath, "Texture/" + colour + ".png");
        Debug.Log(boxColour);
        LoadTextures();

    }

    private void LoadTextures()
    {
        byte[] imageBytes = File.ReadAllBytes(boxColour);

       //create a temporary texture to hold our texture in 
        Texture2D texture = new Texture2D(2, 2);
        //takes the byte in and convert it into an image 
        texture.LoadImage(imageBytes);

        GetComponent<Renderer>().material.mainTexture = texture;
    }
}
