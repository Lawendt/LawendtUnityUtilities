using UnityEngine;

namespace LUT.Primitive
{
	public sealed class BoolData : ScriptableObject
	{
		[SerializeField]
		private bool _value;

		public bool Value
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

		public static implicit operator bool(BoolData f)
		{
			return f.Value;
		}

	}
}
