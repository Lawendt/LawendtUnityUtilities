using UnityEngine.Events;

namespace LUT
{

	public class UnityEventOnGameObjectCallback : OnGameObjectCallback
	{
		public UnityEvent execute;
		public override void Execute()
		{
			execute.Invoke();
		}
	}
}
