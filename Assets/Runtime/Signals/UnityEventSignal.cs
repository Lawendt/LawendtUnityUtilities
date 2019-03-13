using UnityEngine;
using UnityEngine.Events;

namespace LUT
{
    public abstract class UnityEventSignal<T> : MonoBehaviour where T : ASignal, new()
    {
        [SerializeField]
        private UnityEvent _onDispatch = new UnityEvent();

        private void Start()
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

    public abstract class UnityEventSignal<Type, SignalType, UnityEventType> : MonoBehaviour
        where SignalType : ASignal<Type>, new()
        where UnityEventType : UnityEvent<Type>, new()
    {
        [SerializeField]
        private UnityEventType _onDispatch = new UnityEventType();

        private void Start()
        {
            Signals.Get<SignalType>().AddListener(Dispatch);
        }

        private void OnDisable()
        {
            Signals.Get<SignalType>().RemoveListener(Dispatch);
        }

        private void Dispatch(Type t)
        {
            _onDispatch.Invoke(t);
        }
    }
}
