using UnityEngine;

namespace LUT
{

	/// <summary>
	/// Add parse functions to the string.
	/// Developed by Lawendt. 
	/// Available @ https://github.com/Lawendt/UnityLawUtilities
	/// </summary>
	public static class ExtendedParse
	{

		#region Color 

		/// <summary>
		/// Parse a color in the format:
		/// "RGBA(0.5f,0.5f,0.5f,0.5f)"
		/// </summary>
		static public Color toColor(this string rString)
		{
			///RGBA(0.5f,0.5f,0.5f,0.5f)
			string[] temp = rString.Substring(5, rString.Length - 6).Split(',');
			Color renderer = Color.white;
			for (int i = 0; i < temp.Length; i++)
			{
				renderer[i] = float.Parse(temp[i]);
			}

			return renderer;
		}

		#endregion

		/// <summary>
		/// Parse a vector4 from the format:
		/// "(0.5f,0.5f,0.5f,0.5f)"
		/// </summary>
		#region Vector
		static public Vector4 toVector4(this string rString)
		{
			string[] temp = rString.Substring(1, rString.Length - 2).Split(',');
			Vector4 renderer = new Vector4(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]), float.Parse(temp[4]));

			return renderer;
		}

		/// <summary>
		/// Parse a vector3 from the format:
		/// "(0.5f,0.5f,0.5f)"
		/// </summary>
		static public Vector3 toVector3(this string rString)
		{
			string[] temp = rString.Substring(1, rString.Length - 2).Split(',');
			Vector3 renderer = new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));

			return renderer;
		}

		/// <summary>
		/// Parse a vector2 from the format:
		/// "(0.5f,0.5f)"
		/// </summary>
		static public Vector2 toVector2(this string rString)
		{
			string[] temp = rString.Substring(1, rString.Length - 2).Split(',');
			Vector2 renderer = new Vector3(float.Parse(temp[0]), float.Parse(temp[1]));

			return renderer;
		}

		#endregion

		/// <summary>
		/// Parse a enum from the string.
		/// </summary>
		#region Enum
		public static T ToEnum<T>(this string value, bool ignoreCase = true)
		{
			return (T)System.Enum.Parse(typeof(T), value, ignoreCase);
		}

		#endregion
	}
}
