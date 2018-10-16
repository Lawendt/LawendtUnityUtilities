using System.Collections.Generic;
using UnityEngine;

namespace LUT
{
	public sealed class IgnoreColliders : MonoBehaviour
	{
#pragma warning disable 0414
		/// <summary>
		/// Radius to add to ignore list
		/// </summary>
		[SerializeField]
		private float _ignoreRadius = 0.1f;
#pragma warning restore 0414

		/// <summary>
		/// Cached colliders to ignored
		/// </summary>
		[SerializeField]
		[InspectorReadOnly]
		private List<Collider2D> _ignoredColliders = new List<Collider2D>();

		private void Awake()
		{
			Collider2D[] colliders = GetComponents<Collider2D>();

			for (int i = 0; i < _ignoredColliders.Count; i++)
			{
				if (_ignoredColliders[i] == null)
				{
					continue;
				}

				for (int j = 0; j < colliders.Length; j++)
				{
					if (colliders[j] != null)
					{
						Physics2D.IgnoreCollision(colliders[j], _ignoredColliders[i]);
					}
				}
			}
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			Collider2D[] overlapped = Physics2D.OverlapCircleAll(transform.position, _ignoreRadius);
			_ignoredColliders = new List<Collider2D>(overlapped);
		}

		/// <summary>
		/// Draw the radius of collision ignore
		/// </summary>
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, _ignoreRadius);
		}
#endif
	}
}
