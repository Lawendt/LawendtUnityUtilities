using UnityEngine;

namespace LUT.Events.Trigger
{
	public sealed class TriggerEventOnCollisionEnter2D : TriggerEventOn
	{
		public void OnCollisionEnter2D(Collision2D collision)
		{
			if (string.IsNullOrEmpty(_targetTag) || collision.gameObject.CompareTag(_targetTag))
			{
				_eventToTrigger.Invoke();
				this.enabled = false;
			}
		}
	}
}
