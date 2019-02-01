namespace LUT.Events.Primitives
{
	public sealed class EventBool : EventObject<bool>
	{
#if UNITY_EDITOR
#pragma warning disable 0414
		[UnityEngine.SerializeField]
		private bool valueToInvokeWith = false;
#endif
	}
}