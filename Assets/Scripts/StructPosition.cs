using UnityEngine;
using UnityEngine.UIElements;

public struct StructPosition
{
    public float X { get; }
    public float Y { get; }
    public float Z { get; }

    public StructPosition(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
