using NUnit.Framework.Internal.Filters;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public Rigidbody boxBody;
    [SerializeField] private float beltSpeed = 1.0f;
    
   
    private void OnCollisionEnter(Collision detect)
    {

        boxBody.transform.rotation = detect.transform.rotation;
        //issue with centering item this may be removed but it still works fine just has the boxobject close to edge.

        if (detect.transform.tag == "SortingBelt")
        {
            CheckSwitch();
        }
    }

    private void OnCollisionStay(Collision detect)
    {
        if (detect.transform.tag == "ConveyorBelt")
        {
            float step = beltSpeed * Time.deltaTime;
            boxBody.MovePosition(boxBody.position + detect.transform.forward * step);
        } 
        
    }
    void CheckSwitch()
    {
        //compare box ID to belt-BoxID
        //if they match -> redirect box or delete box or teleport it 
    }
}
