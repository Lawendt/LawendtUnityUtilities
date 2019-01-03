using UnityEngine;

namespace LUT.Example
{
	public class ShowIfExample : MonoBehaviour
	{
		public enum Dimension
		{
			Two = 2, Three = 3, Four = 4
		}
		public Dimension currentDimension;
		[ShowIf("currentDimension", Dimension.Two)]
		public Vector2 vector2;
		[ShowIf("currentDimension", (Dimension)3)]
		public Vector3 vector3;
		[ShowIf("currentDimension", Dimension.Four)]
		public int x, y, z, w;
	}
}
