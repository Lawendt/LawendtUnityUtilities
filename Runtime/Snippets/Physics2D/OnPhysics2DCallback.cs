using UnityEngine;

namespace LUT
{
	public abstract class OnPhysics2DCallback : MonoBehaviour
	{
		[SerializeField]
		private LayerMask _layerMask = new LayerMask();

		public enum GameObjectEvent
		{
			OnTriggerEnter2D,
			OnTriggerExit2D,
			OnCollisionEnter2D,
			OnCollisionExit2D,
		}

		public GameObjectEvent executeOn;


		public abstract void Execute();

		#region Callbacks

		public virtual void OnTriggerEnter2D(Collider2D collision)
		{
			CheckExecution(GameObjectEvent.OnTriggerEnter2D, collision.gameObject.layer);
		}

		public virtual void OnTriggerExit2D(Collider2D collision)
		{
			CheckExecution(GameObjectEvent.OnTriggerExit2D, collision.gameObject.layer);
		}

		public virtual void OnCollisionEnter2D(Collision2D collision)
		{
			CheckExecution(GameObjectEvent.OnCollisionEnter2D, collision.gameObject.layer);
		}

		public virtual void OnCollisionExit2D(Collision2D collision)
		{
			CheckExecution(GameObjectEvent.OnCollisionExit2D, collision.gameObject.layer);
		}

		private bool CheckLayer(int otherLayer)
		{
			otherLayer = 1 << otherLayer; // shift 1 to the left value of otherlayer times
			return (otherLayer & _layerMask) != 0;
		}

		private void CheckExecution(GameObjectEvent e, int layer)
		{
			if (e == executeOn && CheckLayer(layer))
			{
				Execute();
			}
		}

		#endregion
	}
}
