using UnityEngine.Events;

namespace LUT
{

	public class UnityEventOnPhysics2DCallback : OnPhysics2DCallback
	{
		public UnityEvent execute;
		public override void Execute()
		{
			execute.Invoke();
		}
	}
}
