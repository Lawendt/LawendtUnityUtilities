using UnityEngine;

namespace LUT
{
	public class FloatArrayData : ScriptableObject
	{
		[SerializeField]
		private int _lengthData = 0;

		public float[] data = new float[0];


		public int LengthData
		{
			get
			{
				return _lengthData;
			}

			set
			{
				if (_lengthData != value)
				{
					data = new float[value];
				}

				_lengthData = value;
			}
		}


		public float this[int i]
		{
			get
			{
				return data[i];
			}
			set
			{
				data[i] = value;
			}
		}

	}
}
