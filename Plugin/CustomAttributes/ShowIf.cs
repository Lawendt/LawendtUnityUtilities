using UnityEngine;

namespace LUT
{
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
	///[ShowIf("currentDimension", Dimension.Two)]
	///public Vector2 vector2;
	///[ShowIf("currentDimension", (Dimension)3)]
	///public Vector3 vector3;
	///[ShowIf("currentDimension", Dimension.Four)]
	///public int x, y, z, w;
	///
	/// </code>
	/// </example>
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
	public class ShowIf : PropertyAttribute
	{
		public enum ShowIfType
		{
			FieldEquals,
			Method,
			FieldNotEquals
		}
		public ShowIfType Type;
		public string ValidateMethod;
		public object Value;

		public ShowIf(string validateMethod, object value = null, ShowIfType type = ShowIfType.FieldEquals)
		{
			this.ValidateMethod = validateMethod;
			this.Value = value;
			this.Type = type;
		}

	}
}