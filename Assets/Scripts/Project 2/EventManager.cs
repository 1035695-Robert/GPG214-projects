using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    //if box is infont of boxLoader 
    // dont spawn new box

    //move box
    // collision will rotate box on enter
    public delegate void BoxMove();
    public static BoxMove boxMove;

    public delegate void BoxDetection();
    public static BoxDetection boxDetection;
   
}
