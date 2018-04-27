using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnityEventSignal<T> : MonoBehaviour where T : ASignal, new()
{
    [SerializeField]
    private UnityEvent _onDispatch;
    // Use this for initialization
    void Start()
    {
        Signals.Get<T>().AddListener(Dispatch);
    }

    private void OnDisable()
    {
        Signals.Get<T>().RemoveListener(Dispatch);
    }

    private void Dispatch()
    {
        _onDispatch.Invoke();
    }
}
