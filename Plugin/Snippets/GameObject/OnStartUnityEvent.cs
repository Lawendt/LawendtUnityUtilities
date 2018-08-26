using UnityEngine;
using UnityEngine.Events;

public sealed class OnStartUnityEvent : MonoBehaviour
{
    public UnityEvent onStart = new UnityEvent();
    public void Start()
    {
        onStart.Invoke();
    }
}
