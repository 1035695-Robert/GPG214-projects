using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    int waittime = 2;   

    public IEnumerator Start()
    {
       //check if theres assetbundle
     

        yield return EventManager.generateLevel.Invoke();
       
        //load textures
        yield return EventManager.objectPool.Invoke();
        
        //
        yield return StartCoroutine(TimeTracker());
        yield break;
    }

    IEnumerator TimeTracker()
    {
        while (true)
        {
            EventManager.boxDetection.Invoke();
            yield return new WaitForSeconds(waittime);
        }
    }
}

