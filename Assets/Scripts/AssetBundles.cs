using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using UnityEngine;

public class AssetBundles : MonoBehaviour
{
    string folderPath = "AssetBundles";
    string fileName = "boxbundle";
    public string combinePath;

   public GameObject boxPrefab;

    public AssetBundle boxBundle;

    void Awake()
    {
        StartCoroutine(LoadAssetBundle());
    }
    void Update()
    {

    }
    public void LoadBox(string Name)
    {
        if (boxBundle == null)
        {
            return;
        }

        boxPrefab = boxBundle.LoadAsset<GameObject>(Name);
        Texture2D texture = boxBundle.LoadAsset<Texture2D>("Yellow");
        
        Debug.Log(boxPrefab);
    }

    IEnumerator LoadAssetBundle()
    {
        combinePath = Path.Combine(Application.streamingAssetsPath, folderPath, fileName);

        if (File.Exists(combinePath))
        {
            var request = AssetBundle.LoadFromFileAsync(combinePath);
            yield return request;

            boxBundle = request.assetBundle;
            Debug.Log("asset bundle loaded");
      
        }
        else
        {
            Debug.LogError("file does not exist" + combinePath);
            yield break;
        }
    }
}
