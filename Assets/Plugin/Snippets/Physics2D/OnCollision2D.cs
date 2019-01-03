using UnityEngine;
using UnityEngine.Events;

public sealed class OnCollision2D : MonoBehaviour
{
	[SerializeField]
	private LayerMask _layerMask = new LayerMask();
	public UnityEvent OnCollision = new UnityEvent();

	private void OnCollisionEnter2D(Collision2D collision)
	{
		int otherLayer = collision.collider.gameObject.layer;
		otherLayer = 1 << otherLayer; // shift 1 to the left value of otherlayer times
		if ((otherLayer & _layerMask) != 0)
		{
			OnCollision.Invoke();
		}
	}
}
