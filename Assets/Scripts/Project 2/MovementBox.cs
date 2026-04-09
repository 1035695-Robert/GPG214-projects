using System.Collections;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class MovementBox : MonoBehaviour
{
    public bool isMoving;
    public float waitTime = 2;
     float distance = 2.0f;
  public float speed = 2.0f;

    public void OnEnable()
    {
        EventManager.boxMove += MoveAllBox;
    }

    private void OnDisable()
    {
        EventManager.boxMove -= MoveAllBox;
    }
    void MoveAllBox()
    {
        if(gameObject != null)
        StartCoroutine(StartMove()); 
        Debug.Log("box is moved");
    }
    IEnumerator StartMove()
    {
        isMoving = true;
        yield return new WaitForSeconds(waitTime);
        Vector3 moveDirection = transform.forward;
        Vector3 startPosition = transform.position;

        Vector3 endPosition = startPosition + (moveDirection * distance);

        //Debug.Log("Start: "+ startPosition+ " End: " + endPosition + "Rotation: "+ item.rotation);
        float elapsed = 0;

        while (elapsed < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsed);
            elapsed += Time.deltaTime * speed;
            yield return null;
        }
        transform.position = endPosition;
        isMoving = false;

    }
}
