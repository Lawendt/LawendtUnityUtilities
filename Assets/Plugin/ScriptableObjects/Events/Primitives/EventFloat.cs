using UnityEngine;

namespace LUT.Events.Primitives
{
	[CreateAssetMenu(fileName = "EventFloat", menuName = "Event/Float", order = 210)]
	public sealed class EventFloat : EventObject<float>
	{
#if UNITY_EDITOR
#pragma warning disable 0414
		[SerializeField]
		private float valueToInvokeWith = 0;
#endif
	}
}