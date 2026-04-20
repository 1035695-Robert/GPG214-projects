using System.Collections;
using System.IO;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class TextureLoadAsync : MonoBehaviour
{
    [SerializeField] private Texture texture;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    AssetBundles bundle;
    private void Awake()
    {
        bundle = GameObject.Find("assetBundle").GetComponent<AssetBundles>();
    
    }
    public IEnumerator FilePath(string boxColor)
    {
        if (boxColor == null)
        {
            Debug.LogError("no Texture"); 
        }
  

        string filePath = Path.Combine(Application.streamingAssetsPath, "Texture/" + boxColor + ".png");

        if (!File.Exists(filePath))
        {
            filePath = bundle.combinePath;
            
         
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
            Debug.LogWarning("error with downloading file" + imageRequest);
            
            yield break;
        }

        Debug.Log(transform.name + ": download complete");

        byte[] allDataDownloaded = imageRequest.downloadHandler.data;
        Texture2D myTexture = new Texture2D(2, 2);


        myTexture.LoadImage(allDataDownloaded);


        texture = myTexture;

        //box.GetComponent<Renderer>().material.mainTexture = texture;

        imageRequest.Dispose();
        //best practice as it frees up memory
        yield return null;
    }
    public Texture ReturnTexture()
    {
       
        return texture;
    }
}

