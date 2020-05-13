namespace UnityEngine
{
	public static class VectorExtensions
	{

		public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
		{
			return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
		}

		public static Vector3 Sum(this Vector3 original, float? x = null, float? y = null, float? z = null)
		{
			return new Vector3(x != null ? original.x + x.Value : original.x,
							   y != null ? original.y + y.Value : original.y,
							   z != null ? original.z + z.Value : original.z);
		}

		public static Vector3 Sum(this Vector3 original, float value)
		{
			return new Vector3(original.x + value,
							   original.y + value,
							   original.z + value);
		}

		public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
		{
			return Vector3.Normalize(destination - source);
		}

		public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
		{
			return new Vector2(x ?? original.x, y ?? original.y);
		}

		public static Vector2 DirectionTo(this Vector2 source, Vector2 destination)
		{
			return (destination - source).normalized;
		}
	}
}
