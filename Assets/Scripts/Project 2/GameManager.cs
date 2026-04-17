using System.Collections;
using System.Threading;
using Unity.Android.Types;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    int waittime = 2;   

    public IEnumerator Start()
    {
       
        yield return EventManager.generateLevel.Invoke();
        //load textures
        yield return EventManager.objectPool.Invoke();
        
        yield return StartCoroutine(TimeTracker());
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

