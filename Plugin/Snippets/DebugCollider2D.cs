using UnityEngine;

namespace LUT.Snippets
{
	/// <summary>
	/// Use this too see the Collider2DGizmos when it is not available for some reason.
	/// </summary>
	public sealed class DebugCollider2D : MonoBehaviour
	{
		public void OnDrawGizmosSelected()
		{
			Collider2D[] collider = GetComponents<Collider2D>();

			Gizmos.color = Color.green;
			foreach (var c in collider)
			{
				if (c is BoxCollider2D)
				{
					BoxCollider2D box = (BoxCollider2D)c;
					Vector2 topLeft = transform.position;
					topLeft.x += -box.size.x / 2.0f + box.offset.x;
					topLeft.y += -box.size.y / 2.0f + box.offset.y;

					Vector2 topRight = transform.position;
					topRight.x += box.size.x / 2.0f + box.offset.x;
					topRight.y += -box.size.y / 2.0f + box.offset.y;

					Vector2 bottomRight = transform.position;
					bottomRight.x += box.size.x / 2.0f + box.offset.x;
					bottomRight.y += box.size.y / 2.0f + box.offset.y;

					Vector2 bottomLeft = transform.position;
					bottomLeft.x += -box.size.x / 2.0f + box.offset.x;
					bottomLeft.y += box.size.y / 2.0f + box.offset.y;


					Gizmos.DrawLine(topLeft, topRight);
					Gizmos.DrawLine(topRight, bottomRight);
					Gizmos.DrawLine(bottomRight, bottomLeft);
					Gizmos.DrawLine(bottomLeft, topLeft);
				}
				else if (c is PolygonCollider2D)
				{
					PolygonCollider2D poly = (PolygonCollider2D)c;
					Vector2 pos = transform.position;
					for (int i = 0; i < poly.points.Length; i++)
					{
						Gizmos.DrawLine(pos + poly.points[i], pos + poly.points[(i + 1) % poly.points.Length]);
					}
				}
			}
		}
	}
}
