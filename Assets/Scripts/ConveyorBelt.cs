using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class ConveyorBelt : MonoBehaviour
{
    Transform moveDirection;

    [SerializeField] private float distance = 2.5f;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float waitTime = 2f;
    private bool isMoving = false;

    private Rigidbody detectedBox;

    private void OnTriggerEnter(Collider detect)
    {
        if (detect.GetComponent<Rigidbody>() != null)
        {
            detect.transform.rotation = transform.rotation;
        }
    }
    private void OnTriggerStay(Collider detect)
    { 
        if (!isMoving && detect.GetComponent<Rigidbody>() != null )
        {
             StartCoroutine(MoveItem(detect.transform));
        }
    }

    IEnumerator MoveItem(Transform item)
    {
        isMoving = true;
        yield return new WaitForSeconds(waitTime); 
        Vector3 moveDirection = transform.forward;
        Vector3 startPosition = item.position;

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
