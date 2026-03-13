using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using UnityEngine;
using System;

public class AssetBundles : MonoBehaviour
{
    string folderPath = "AssetBundles";
    string fileName = "boxbundle";
    public string combinePath;

    public GameObject[] boxPrefabs;

    public AssetBundle boxBundle;

    void Awake()
    {
        LoadAssetBundle();
    }
    private void LoadAssetBundle()
    {
        try
        {
            combinePath = Path.Combine(Application.streamingAssetsPath, folderPath, fileName);

            if (File.Exists(combinePath))
            {
                var request = AssetBundle.LoadFromFileAsync(combinePath);
                boxBundle = request.assetBundle;

                if (boxBundle == null)
                {
                    Debug.LogError("failed to load AssetBundle");
                }

                boxPrefabs = boxBundle.LoadAllAssets<GameObject>();
                Debug.Log(boxPrefabs.ToString());

            }
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError("file Not Found:" + e.Message);
        }
        catch (Exception e)
        {
            Debug.LogError("an Error Has Occurrred: " + e.Message);
        }
        finally
        {
            if (boxBundle != null)
            {
                boxBundle.Unload(false);
                Debug.Log("bundle memory cleaned up.");

            }
        }
    }
}
