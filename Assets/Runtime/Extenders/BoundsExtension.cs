using UnityEngine;

public static class BoundsExtension
{
	public static float GetFittingScale(this Bounds bound, Vector3 currentScale, Vector2 minSize, Vector2 maxSize)
	{
		Vector2 currentSize = bound.size;
		Vector2 newSize = Vector2.zero;

		newSize.x = Mathf.Clamp(currentSize.x, minSize.x, maxSize.x);
		newSize.y = Mathf.Clamp(currentSize.y, minSize.y, maxSize.y);

		return bound.GetFittingScaleFor(currentScale, newSize);
	}

	public static float GetFittingScaleFor(this Bounds bound, Vector3 currentScale, Vector2 newSize)
	{
		Vector2 currentSize = bound.size;
		Vector3 newScale = Vector3.zero;

		newScale.x = currentScale.x * newSize.x / currentSize.x;
		newScale.y = currentScale.y * newSize.y / currentSize.y;

		return Mathf.Min(newScale.x, newScale.y);
	}

	public static float GetFittingScaleFactor(this Bounds bound, Vector2 minSize, Vector2 maxSize)
	{
		Vector2 currentSize = bound.size;
		Vector2 newSize = Vector2.zero;
		Vector3 newScale = Vector3.zero;

		newSize.x = Mathf.Clamp(currentSize.x, minSize.x, maxSize.x);
		newSize.y = Mathf.Clamp(currentSize.y, minSize.y, maxSize.y);

		return bound.GetFittingScaleFactorFor(newSize);
	}

	public static float GetFittingScaleFactorFor(this Bounds bound, Vector2 newSize)
	{
		Vector2 currentSize = bound.size;
		Vector3 newScale = Vector3.zero;

		newScale.x = newSize.x / currentSize.x;
		newScale.y = newSize.y / currentSize.y;

		return Mathf.Min(newScale.x, newScale.y);
	}
}
