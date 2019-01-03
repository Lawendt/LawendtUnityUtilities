using UnityEngine;
using UnityEngine.Events;

namespace LUT.Events
{

	public class EventMonoBehaviour : MonoBehaviour
	{
		[SerializeField]
		private EventObject _targetEvent;
#if UNITY_EDITOR
		private EventObject _cacheEvent;
#endif
		[SerializeField]
		private UnityEvent _onInvoke;

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (_cacheEvent != null && Application.isPlaying)
			{
				_cacheEvent.Unregister(_onInvoke.Invoke);
			}
			_cacheEvent = _targetEvent;
		}
#endif

		public void Reset()
		{
			_onInvoke = new UnityEvent();
			_targetEvent = null;
		}
		public void OnEnable()
		{
			if (_targetEvent == null)
			{
				Debug.LogError("No event on EventInspector of " + name + ". Disabling itself");
				enabled = false;
				return;
			}
#if UNITY_EDITOR
			_cacheEvent = _targetEvent;
#endif
			_targetEvent.Register(_onInvoke.Invoke);
		}

		public void OnDisable()
		{
			_targetEvent.Unregister(_onInvoke.Invoke);
		}
	}
}
