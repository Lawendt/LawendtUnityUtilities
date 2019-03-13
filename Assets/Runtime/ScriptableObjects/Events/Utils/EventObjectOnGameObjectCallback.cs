using UnityEngine.Events;

namespace LUT.Events
{
	public class EventObjectOnGameObjectCallback : OnGameObjectCallback
	{
		public EventObject execute;
		public override void Execute()
		{
			execute.Invoke();
		}
	}
}
