using UnityEngine;
using UnityEditor;
using System.IO;
public class CreateAssetBundle
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectiory = Path.Combine(Application.streamingAssetsPath, "AssetBundles");

        if(Directory.Exists(assetBundleDirectiory))
        {
            Directory.CreateDirectory(assetBundleDirectiory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectiory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}

