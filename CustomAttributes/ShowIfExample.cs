using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIfExample : MonoBehaviour
{
    public enum Dimension
    {
       Two = 2, Three = 3, Four = 4
    }
    public Dimension currentDimension;
    [ShowIf("IsDimension", Dimension.Two)]
    public Vector2 vector2;
    [ShowIf("IsDimension", 3)]
    public Vector3 vector3;
    [ShowIf("IsDimension", Dimension.Four)]
    public int x, y, z, w;

    public bool IsDimension(Dimension value)
    {
        return value == currentDimension;
    }
}
