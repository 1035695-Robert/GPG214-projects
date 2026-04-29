using Unity.VisualScripting;
using UnityEngine;

public class BoxLoaderRaycasts
{

    public static RaycastHit[] result = new RaycastHit[10];
    public RaycastHit[] ray = new RaycastHit[10];
    public void Update()
    {
        ray = result;
    } }
