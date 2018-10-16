using UnityEngine;
using UnityEngine.Events;

namespace LUT
{

	public sealed class OnEnableUnityEvent : MonoBehaviour
	{
		public UnityEvent onEnable = new UnityEvent();

		public void OnEnable()
		{
			onEnable.Invoke();
		}
	}
}
