using UnityEngine;

namespace LUT
{
	public abstract class OnGameObjectCallback : MonoBehaviour
	{
		public enum GameObjectCallback
		{
			None,
			OnStart,
			OnDestroy,
			OnEnable,
			OnDisable
		}

		public GameObjectCallback executeOn;

		public void Reset()
		{
			executeOn = GameObjectCallback.None;
		}

		public abstract void Execute();

		#region GameObject Events
		public virtual void Start()
		{
			CheckExecution(GameObjectCallback.OnStart);
		}
		public virtual void OnDestroy()
		{
			CheckExecution(GameObjectCallback.OnDestroy);
		}

		public virtual void OnEnable()
		{
			CheckExecution(GameObjectCallback.OnEnable);
		}

		public virtual void OnDisable()
		{
			CheckExecution(GameObjectCallback.OnDisable);
		}

		private void CheckExecution(GameObjectCallback e)
		{
			if (e == executeOn)
			{
				Execute();
			}
		}

		#endregion
	}
}
