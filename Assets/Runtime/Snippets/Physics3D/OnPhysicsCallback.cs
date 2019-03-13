using UnityEngine;

namespace LUT
{
	public abstract class OnPhysicsCallback : MonoBehaviour
	{
		[SerializeField]
		private LayerMask _layerMask = new LayerMask();

		public enum GameObjectEvent
		{
			OnTriggerEnter,
			OnTriggerExit,
			OnCollisionEnter,
			OnCollisionExit,
		}

		public GameObjectEvent executeOn;


		public abstract void Execute();

		#region Callbacks

		public virtual void OnTriggerEnter(Collider collision)
		{
			CheckExecution(GameObjectEvent.OnTriggerEnter, collision.gameObject.layer);
		}

		public virtual void OnTriggerExit(Collider collision)
		{
			CheckExecution(GameObjectEvent.OnTriggerExit, collision.gameObject.layer);
		}

		public virtual void OnCollisionEnter(Collision collision)
		{
			CheckExecution(GameObjectEvent.OnCollisionEnter, collision.gameObject.layer);
		}

		public virtual void OnCollisionExit(Collision collision)
		{
			CheckExecution(GameObjectEvent.OnCollisionExit, collision.gameObject.layer);
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
