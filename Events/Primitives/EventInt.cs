using UnityEngine;

namespace LUT.Event.Primitives
{
    [CreateAssetMenu(fileName = "EventInt", menuName = "Event/Int", order = 200)]
    public sealed class EventInt : LUT.Event.EventObject<int>
    {
    }
}