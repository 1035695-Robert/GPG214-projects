using System.Collections;
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

    public delegate IEnumerator GenerateLevel();
    public static GenerateLevel generateLevel;

    public delegate IEnumerator ObjectPool();
    public static ObjectPool objectPool;

    public delegate void PlayerSelection(PlayerID player);
    public static PlayerSelection setPlayer;

    public delegate void TextureLoad(GameObject targetItem);
    public static TextureLoad ItemTextureLoad;

}
