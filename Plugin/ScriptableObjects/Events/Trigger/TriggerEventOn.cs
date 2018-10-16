using UnityEngine;

namespace LUT.Events.Trigger
{
	public abstract class TriggerEventOn : MonoBehaviour
	{
		[Header("Setup")]
		[SerializeField]
		[Tooltip("Event invoked when target enters trigger")]
		protected EventObject _eventToTrigger;

		[SerializeField]
		[Tooltip("Tag of the object that will cause the trigger")]
		[TagSelector]
		protected string _targetTag;

		public void Reset()
		{
			_eventToTrigger = null;
			_targetTag = null;
		}
	}
}
