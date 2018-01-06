using System;
using UnityEngine;

/// <summary>
/// Public fields that you want to show in inspector, but only on certain conditions.
/// Developed by @la_wendt
/// 
/// <example> 
/// In this example, the inspector will only show the value 
/// </code>
///public enum Dimension
///{
///    Two = 2, Three = 3, Four = 4
///}
///public Dimension currentDimension;
///[ShowIf("IsDimension", Dimension.Two)]
///public Vector2 vector2;
///[ShowIf("IsDimension", 3)]
///public Vector3 vector3;
///[ShowIf("IsDimension", Dimension.Four)]
///public int x, y, z, w;
///
///public bool IsDimension(Dimension value)
///{
///    return value == currentDimension;
///}
/// </code>
/// </example>
/// </summary>
public class ShowIf : PropertyAttribute
{
    public string ValidateMethod;
    public object Value;

    public ShowIf(string validateMethod, object value = null)
    {
        this.ValidateMethod = validateMethod;
        this.Value = value;
    }

}