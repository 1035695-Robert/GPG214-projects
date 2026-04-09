using System.Collections;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
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
            EventManager.boxDetection.Invoke();
            yield return new WaitForSeconds(waittime);
        }
    }
}

