using UnityEngine;

namespace LUT.Snippets.Physics2D
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class RigidbodyVelocityDebug : MonoBehaviour
	{
		[SerializeField, HideInInspector]
		private Rigidbody2D _target;

		public void Start()
		{
			_target = GetComponent<Rigidbody2D>();
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			if (_target == null)
			{
				_target = GetComponent<Rigidbody2D>();
			}

			Gizmos.DrawRay(_target.transform.position, _target.velocity);
		}
	}
}
