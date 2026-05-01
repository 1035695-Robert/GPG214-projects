using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AsyncTextureLoad : MonoBehaviour
{
    [SerializeField] private string textureName;

    [SerializeField] private Sprite spriteImage;
    [SerializeField] private Texture texture;


    private IEnumerator Start()
    {
        yield return StartCoroutine(FilePath(textureName));
    }
    public IEnumerator FilePath(string boxColor)
    {
        if (boxColor != null)
        {
            textureName = boxColor;
        }
        else
        {
            Debug.LogError("no Texture");
        }

        string filePath = Path.Combine(Application.streamingAssetsPath, "Texture/" + textureName + ".png");
        if (!File.Exists(filePath))
        {
            yield break;
        }
        yield return StartCoroutine(LoadTextureFromFile(filePath));
    }
    public IEnumerator LoadTextureFromFile(string filePath)
    {

        UnityWebRequest imageRequest = UnityWebRequest.Get(filePath);

        AsyncOperation downloadOperation = imageRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log(transform.name + ": download progress: " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }
        if (imageRequest.result == UnityWebRequest.Result.ConnectionError || imageRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("error with downloading file" + imageRequest);
            GetComponent<Renderer>().material.color = Color.magenta;
            yield break;
        }

        Debug.Log(transform.name + ": download complete");

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



    //public IEnumerator LoadTextureFromFile(string path)
    //{

    //    string filePath = Path.Combine(path);

    //    if (File.Exists(filePath))
    //    {
    //        byte[] imageData = File.ReadAllBytes(filePath);

    //        Texture2D texture = new Texture2D(2, 2);
    //        texture.LoadImage(imageData);

    //        GetComponent<Renderer>().material.mainTexture = texture;
    //        Debug.Log("texture is found");
    //        yield return null;
    //    }
    //    else
    //    { 
    //        GetComponent<Renderer>().material.color = Color.magenta;
    //        yield break;
    //    }
    //}


}