using UnityEngine;

namespace LUT.Events.Primitives
{
	[CreateAssetMenu(fileName = "EventInt", menuName = "Event/Int", order = 200)]
	public sealed class EventInt : LUT.Events.EventObject<int>
	{
	}
}