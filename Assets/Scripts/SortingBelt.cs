using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class SortingBelt : Belts
{
    [SerializeField] string[] targetObjectName;
    [SerializeField] float angleToRotate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>() != null && targetObjectName.Contains(other.name))
        {
            other.transform.Rotate(Vector3.up, angleToRotate);
        }
    }
    private void OnTriggerStay(Collider detectedObject)
    {
        if (!isMoving && detectedObject.GetComponent<Rigidbody>() != null)
        {
            StartCoroutine(MoveItem(detectedObject.transform));
        }
    }
}
