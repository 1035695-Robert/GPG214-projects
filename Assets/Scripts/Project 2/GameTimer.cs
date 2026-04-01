using System.Collections;
using System.Threading;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    float waittime = 2f;


    public void Start()
    {
        StartCoroutine(TimeTracker());
    }
    IEnumerator TimeTracker()
    {
        while (true)
        {
            Debug.Log("activate event");
            EventManager.BoxDetection.Invoke();
            yield return new WaitForSeconds(waittime);
        }
    }
}

