using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class BoxTypeGenerator : MonoBehaviour
{

    public string jsonFileName;
    public string jsonData;

    public string BoxObjectName;
    public string BoxName;
    public BoxList myBoxList = new BoxList();


    private string streamingAssetsFolderPath = Application.streamingAssetsPath;
    private IEnumerator Start()
    {
        yield return StartCoroutine(LoadLocalJsonAsync());

        yield return StartCoroutine(LoadBoxWithData());
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

        jsonLoadingRequest.Dispose();
        //best practice: frees up memory

        yield return null;

    }

    IEnumerator LoadBoxWithData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "BoxList.JSON");

        string jsonData = File.ReadAllText(filePath);

        myBoxList = JsonUtility.FromJson<BoxList>(jsonData);

        foreach (Box boxFile in myBoxList.box)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + boxFile.name);
            if (prefab != null)
            {
                GameObject boxPrefab = Instantiate(prefab, new Vector3(0, 3, 0), Quaternion.identity);

                BoxInformation boxInformation = prefab.GetComponent<BoxInformation>();
                boxInformation.TextureLoader(boxFile.colour, boxPrefab);
            }
            yield return null;
        }
    }
}

