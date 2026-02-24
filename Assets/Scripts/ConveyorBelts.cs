using System.Collections;
using UnityEngine;

public class ConveyorBelts : Belts
{
    private void OnTriggerEnter(Collider detect)
    {
        if (detect.GetComponent<Rigidbody>() != null)
        {
            detect.transform.rotation = transform.rotation;
        }
    }
    private void OnTriggerStay(Collider detect)
    {
        if (!isMoving && detect.GetComponent<Rigidbody>() != null)
        {
            StartCoroutine(MoveItem(detect.transform));
        }
    }

  
}
