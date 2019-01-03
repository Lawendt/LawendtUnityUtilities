// Create UnityEvent children with primitives and basic unity objects

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

    [System.Serializable]
    public class UnityEventBool : UnityEvent<bool> { }

    [System.Serializable]
    public class UnityEventTransform : UnityEvent<Transform> { }

    [System.Serializable]
    public class UnityEventVector3 : UnityEvent<Vector3> { }

    [System.Serializable]
    public class UnityEventVector2 : UnityEvent<Vector2> { }

}