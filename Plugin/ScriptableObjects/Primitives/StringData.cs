using UnityEngine;

namespace LUT.Primitive
{
	public sealed class StringData : ScriptableObject
	{
		[SerializeField]
		private string _value;

		public string Value
		{
			get
			{
				return _value;
			}

			set
			{
				_value = value;
			}
		}

		public static implicit operator string(StringData f)
		{
			return f.Value;
		}


		public static string operator +(StringData data, string value)
		{
			return data.Value + value;
		}
	}
}
