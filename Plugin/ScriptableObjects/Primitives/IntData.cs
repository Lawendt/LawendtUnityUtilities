using UnityEngine;

namespace LUT.Primitive
{
	public sealed class IntData : ScriptableObject
	{
		[SerializeField]
		private int _value;

		public int Value
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

		public static implicit operator int(IntData f)
		{
			return f.Value;
		}


		public static int operator +(IntData data, int value)
		{
			return data.Value + value;
		}

		public static int operator -(IntData data, int value)
		{
			return data.Value - value;
		}

	}
}
