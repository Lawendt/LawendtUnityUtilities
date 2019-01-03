using UnityEngine;

namespace LUT.Primitive
{
	[CreateAssetMenu(fileName = "FloatData", menuName = "Data/Primitive/Float", order = 100)]
	public sealed class FloatData : ScriptableObject
	{
		[SerializeField]
		private float _value;

		public float Value
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

		public static implicit operator float(FloatData f)
		{
			return f.Value;
		}


		public static float operator +(FloatData data, float value)
		{
			return data.Value + value;
		}

		public static float operator -(FloatData data, float value)
		{
			return data.Value - value;
		}

	}
}
