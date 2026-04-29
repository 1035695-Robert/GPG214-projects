using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    int waittime = 2;

    private void OnEnable()
    {
        EventManager.startGameManager += StartGame;
    }
    private void OnDisable()
    {
       EventManager.startGameManager -= StartGame;
    }
    public IEnumerator StartGame()
    {

        //    //load textures
        yield return EventManager.objectPool.Invoke();

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return StartCoroutine(TimeTracker());
    yield break;
    }

IEnumerator TimeTracker()
{
    while (true)
    {
        EventManager.boxDetection?.Invoke(); //?. null condition operator
        yield return new WaitForSeconds(waittime);
    }
}
}

