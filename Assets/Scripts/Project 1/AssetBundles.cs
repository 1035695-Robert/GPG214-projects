using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Unity.Android.Gradle;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class AssetBundles : MonoBehaviour
{
    string folderPath = "AssetBundles";
    public string fileName = "dlc_bundle";
    public string combinePath;

    public string[] assetPath;

    public AssetBundle dlcBundle;

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
                dlcBundle = request.assetBundle;

                if (dlcBundle == null)
                {
                    Debug.LogError("failed to load AssetBundle");
                }

                 assetPath = dlcBundle.GetAllAssetNames();
           
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
            if (dlcBundle != null)
            {
               //dlcBundle.Unload(false);
                Debug.Log("bundle memory cleaned up.");

            }
        }
    }
}
