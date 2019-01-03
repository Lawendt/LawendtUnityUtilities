using UnityEngine;

namespace LUT.Snippets
{
	public sealed class LinearMovement : MonoBehaviour
	{

		public float speed;
		public Vector3 direction;

		private void Reset()
		{
			speed = 1;
			direction = Vector3.forward;
		}

		private void FixedUpdate()
		{
			transform.position += GetTransformedDirection() * speed * Time.fixedDeltaTime;
		}

		private Vector3 GetTransformedDirection()
		{
			return transform.TransformDirection(direction).normalized;
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawLine(transform.position, transform.position + GetTransformedDirection() * 2);
		}
	}
}
