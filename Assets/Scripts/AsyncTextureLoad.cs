using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static TreeEditor.TextureAtlas;

public class AsyncTextureLoad : MonoBehaviour
{
    [SerializeField] private string textureName;

    [SerializeField] private Sprite spriteImage;
    [SerializeField] private Texture texture;


    IEnumerator Start()
    {
        yield return StartCoroutine(LoadTextureFromFile());
    }

    public void InitiateTextureLoader(string boxColor)
    {
        textureName = boxColor + ".png";
        StartCoroutine(LoadTextureFromFile());
    }
    IEnumerator LoadTextureFromFile()
    {

        UnityWebRequest imageRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "Texture/" + textureName));

        AsyncOperation downloadOperation = imageRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("download progress: " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }
        if (imageRequest.result == UnityWebRequest.Result.ConnectionError || imageRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("error with downloading file");
            yield break;
        }

        Debug.Log("download complete");

        byte[] allDataDownloaded = imageRequest.downloadHandler.data;
        Texture2D myTexture = new Texture2D(2, 2);
        

        myTexture.LoadImage(allDataDownloaded);


        texture = myTexture;
        
        GetComponent<Renderer>().material.mainTexture = texture;

        spriteImage = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);

        imageRequest.Dispose();
        //best practice as it frees up memory
        yield return null;
    }
}



//load texture on gameobject
//string filePath = Path.Combine(Application.streamingAssetsPath, "Texture/" + textureName);

//if (File.Exists(filePath))
//{
//    byte[] imageData = File.ReadAllBytes(filePath);

//    Texture2D texture = new Texture2D(2, 2);
//    texture.LoadImage(imageData);

//    GetComponent<Renderer>().material.mainTexture = texture;
//    Debug.Log("texture is found");
//}
//else
//{
//    Debug.LogError("texture file not found at path " + filePath);
//}
