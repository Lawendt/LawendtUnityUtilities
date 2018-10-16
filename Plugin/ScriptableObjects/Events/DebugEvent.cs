using UnityEngine;

namespace LUT.Events
{
	public sealed class DebugEvent : MonoBehaviour
	{
		public EventObject eventObject;

		public void OnEnable()
		{
			eventObject.Register(Log);
		}
		public void OnDisable()
		{
			eventObject.Unregister(Log);
		}

		public void Log()
		{
			Debug.Log(eventObject.name + " was invoked");
		}
	}
}
