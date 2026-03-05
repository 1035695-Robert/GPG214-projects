using System.IO;
using UnityEngine;

public class AssetBundles : MonoBehaviour
{
    string folderPath = "AssetBundles";
    string fileName = "boxbundle";
    string combinePath;

    private AssetBundle boxBundle;

    void Start()
    {
        LoadAssetBundle();
        LoadBox();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LoadBox()
    {
        if (boxBundle == null)
        {
            return;
        }

        GameObject boxPrefab = boxBundle.LoadAsset<GameObject>("YellowBox");
        Instantiate(boxPrefab);
        Debug.Log(boxPrefab);
    }
    void LoadAssetBundle()
    {
        combinePath = Path.Combine(Application.streamingAssetsPath, folderPath, fileName);

        if (File.Exists(combinePath))
        {
            boxBundle = AssetBundle.LoadFromFile(combinePath);
            Debug.Log("asset bundle loaded");
        }
        else
        {
            Debug.Log("file does not exist" + combinePath);
        }
    }
}
