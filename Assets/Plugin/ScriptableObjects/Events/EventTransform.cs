using UnityEngine;

namespace LUT.Events
{
	/// <summary>
	/// Serialized event that receives a Transform component as parameter
	/// </summary>
	[CreateAssetMenu(fileName = "EventTransform", menuName = "Event/Transform", order = 210)]
	public class EventTransform : EventObject<Transform>
	{
	}
}
