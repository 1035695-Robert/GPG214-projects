using System.Collections;
using System.IO;
using Unity.Android.Gradle;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoxTypeLoaderAsync : MonoBehaviour
{
    [Header("Information Input")]
    public string jsonFileName;
    public string boxName;

    [Header ("Data")]
    [TextArea(1, 5)]
    public string jsonData;
    public string boxColor;
    public Texture texture;
    public Texture2D boxTexture;

    [Header ("boxArray")]
    public BoxList myBoxList = new BoxList();
    BoxLoader boxloader;

    GameObject prefab;

    private string streamingAssetsFolderPath = Application.streamingAssetsPath;
    private IEnumerator Start()
    {
        yield return StartCoroutine(LoadLocalJsonAsync());
        yield return StartCoroutine(LoadLocalSpriteAsync());
    }

    IEnumerator LoadLocalJsonAsync()
    {
        UnityWebRequest jsonLoadingRequest = UnityWebRequest.Get(Path.Combine(streamingAssetsFolderPath, jsonFileName));

        AsyncOperation downloadOperation = jsonLoadingRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("download progress: " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }
        if (jsonLoadingRequest.result == UnityWebRequest.Result.ConnectionError || jsonLoadingRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("error with downloading file");
            yield break;
        }

        Debug.Log("download complete");

        //access the web request acess the download handler and just grab the text;
        jsonData = jsonLoadingRequest.downloadHandler.text;
        
        
        myBoxList = JsonUtility.FromJson<BoxList>(jsonData);
        foreach (Box boxFile in myBoxList.box)
        {
            prefab = Resources.Load<GameObject>("Prefabs/" + boxFile.name);
            if (prefab != null && boxFile.name == boxName)
            {
                Debug.Log("<color=red>" +prefab.name + "</color>");
                boxColor = boxFile.colour;
            }
            else
            {
             // delete from array?
            }
        }
            jsonLoadingRequest.Dispose();
        //best practice: frees up memory
        yield return null;
    }
    IEnumerator LoadLocalSpriteAsync()
    {
        UnityWebRequest imageRequest = UnityWebRequest.Get(Path.Combine(streamingAssetsFolderPath, "Texture/" + boxColor + ".png"));

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

        imageRequest.Dispose();

        yield return null;
    }
}



