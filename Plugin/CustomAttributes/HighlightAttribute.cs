using UnityEngine;

namespace LUT
{
	/// <summary>
	/// Highligh an atribute on the inspector
	/// Developed by LotteMakesStuff.
	/// Available @ https://gist.github.com/LotteMakesStuff/2d3c6dc7a913ed118601db95735574de
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
}