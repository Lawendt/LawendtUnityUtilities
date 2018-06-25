using UnityEngine.Events;
using UnityEngine;

namespace UnityEngine.Events
{
    [System.Serializable]
    public class UnityEventInt : UnityEvent<int> { }

    [System.Serializable]
    public class UnityEventGameObject : UnityEvent<GameObject> { }

    [System.Serializable]
    public class UnityEventString : UnityEvent<string> { }

    [System.Serializable]
    public class UnityEventFloat : UnityEvent<float> { }

}