// ----------------------------------------------------------------------------
// Original from Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;

namespace LUT.Primitive
{
	[Serializable]
	public class FloatReference
	{
		public bool UseConstant = false;
		public float ConstantValue;
		public FloatData Variable;

		public FloatReference()
		{ }

		public FloatReference(float value)
		{
			UseConstant = true;
			ConstantValue = value;
		}

		public float Value
		{
			get { return UseConstant ? ConstantValue : Variable.Value; }
		}

		public static implicit operator float(FloatReference reference)
		{
			return reference.Value;
		}
	}
}