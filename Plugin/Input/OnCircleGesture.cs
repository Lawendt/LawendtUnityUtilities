using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace LUT
{

	public class OnCircleGesture : MonoBehaviour
	{
		private readonly List<Vector2> gesturePoints = new List<Vector2>();
		private int gestureCount;
		public int gesturesNeeded = 2;
		public UnityEvent onGesture = new UnityEvent();
		private void Update()
		{
			if (CircleGestureMade())
			{
				onGesture.Invoke();
			}
		}
		private bool CircleGestureMade()
		{
			bool result = false;

			int pointsCount = gesturePoints.Count;

			if (Input.GetMouseButton(0))
			{
				Vector2 mousePosition = Input.mousePosition;
				if (pointsCount == 0 || (mousePosition - gesturePoints[pointsCount - 1]).magnitude > 10)
				{
					gesturePoints.Add(mousePosition);
					pointsCount++;
				}
			}
			else if (Input.GetMouseButtonUp(0))
			{
				pointsCount = 0;
				gestureCount = 0;
				gesturePoints.Clear();
			}

			if (pointsCount < 10)
			{
				return result;
			}

			float finalDeltaLength = 0;

			Vector2 finalDelta = Vector2.zero;
			Vector2 previousPointsDelta = Vector2.zero;

			for (int i = 0; i < pointsCount - 2; i++)
			{
				Vector2 pointsDelta = gesturePoints[i + 1] - gesturePoints[i];
				finalDelta += pointsDelta;

				float pointsDeltaLength = pointsDelta.magnitude;
				finalDeltaLength += pointsDeltaLength;

				float dotProduct = Vector2.Dot(pointsDelta, previousPointsDelta);
				if (dotProduct < 0f)
				{
					gesturePoints.Clear();
					gestureCount = 0;
					return result;
				}

				previousPointsDelta = pointsDelta;
			}

			int gestureBase = (Screen.width + Screen.height) / 4;

			if (finalDeltaLength > gestureBase && finalDelta.magnitude < gestureBase / 2f)
			{
				gesturePoints.Clear();
				gestureCount++;

				if (gestureCount >= gesturesNeeded)
				{
					gestureCount = 0;
					result = true;
				}
			}

			return result;
		}
	}
}
