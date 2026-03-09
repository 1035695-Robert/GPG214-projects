using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtonManager : MonoBehaviour
{
    //private AsyncTextureLoad textureLoad;
    //private BoxPoolManager boxPoolManager;

    private void Start()
    {

    }
    public void OnButtonClick(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }


    public IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GPG214");

   
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log(transform.name + ": download progress: " + ((asyncLoad.progress / 1f) * 100) + "%");


            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
        }
        
        yield return null;

    }
}
