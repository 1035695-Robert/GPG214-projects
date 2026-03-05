using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Belts :MonoBehaviour
{
    Transform moveDirection;

    [SerializeField] protected float distance = 2.5f;
    [SerializeField] protected float speed = 2.0f;
    protected  float waitTime = 1.6f;
    protected bool isMoving = false;

    private Rigidbody detectedBox;


    protected IEnumerator MoveItem(Transform item)
    {
        isMoving = true;
        yield return new WaitForSeconds(waitTime);
        Vector3 moveDirection = item.transform.forward;
        Vector3 startPosition = item.transform.position;

        Vector3 endPosition = startPosition + (moveDirection * distance);

        //Debug.Log("Start: "+ startPosition+ " End: " + endPosition + "Rotation: "+ item.rotation);
        float elapsed = 0;

        while (elapsed < 1f)
        {
            item.position = Vector3.Lerp(startPosition, endPosition, elapsed);
            elapsed += Time.deltaTime * speed;
            yield return null;
        }
        item.position = endPosition;
        isMoving = false;

    }
}
