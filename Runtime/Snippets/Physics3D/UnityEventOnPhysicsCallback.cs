using UnityEngine.Events;

namespace LUT
{
	public class UnityEventOnPhysicsCallback : OnPhysicsCallback
	{
		public UnityEvent execute;
		public override void Execute()
		{
			execute.Invoke();
		}
	}
}
