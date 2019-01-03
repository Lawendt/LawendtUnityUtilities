using System.Collections.Generic;
using UnityEngine;

namespace LUT.Comparer
{
	public class SortLowestTransform : IComparer<Transform>
	{
		public int Compare(Transform x, Transform y)
		{
			return x.position.y < y.position.y ? -1 : 1;
		}
	}
}
