using UnityEngine;
using UnityEngine.Events;

public sealed class OnEnableUnityEvent : MonoBehaviour
{
    public UnityEvent onEnable = new UnityEvent();

    public void OnEnable()
    {
        onEnable.Invoke();
    }
}
