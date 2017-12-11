// NOTE DONT put in an editor folder

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class HighlightAttribute : PropertyAttribute
{
    public HighlightColor Color;
    public string ValidateMethod;
    public object Value;

    public HighlightAttribute(HighlightColor color = HighlightColor.Yellow, string validateMethod = null, object value = null)
    {
        this.Color = color;
        this.ValidateMethod = validateMethod;
        this.Value = value;
    }
}

public enum HighlightColor
{
    Red,
    Pink,
    Orange,
    Yellow,
    Green,
    Blue,
    Violet,
    White
}