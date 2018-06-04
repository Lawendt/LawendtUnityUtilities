using System;
using System.Collections.Generic;
using UnityEngine;

namespace LUT
{
    /// <summary>
    /// Serialized event
    /// </summary>
    [CreateAssetMenu(fileName = "Event", menuName = "Event/Default")]
    public class Event : ScriptableObject
    {
        [SerializeField]
        private List<Action> actions = new List<Action>();

        /// <summary>
        /// How many actions registered.
        /// </summary>
        public int Count
        {
            get { return actions.Count; }
        }

        /// <summary>
        /// Adds a listener to this Event
        /// </summary>
        /// <param name="handler">Method to be called when event is fired</param>
        public void Register(Action handler)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Assert(handler.Method.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), inherit: false).Length == 0,
                "Adding anonymous delegates as Signal callbacks is not supported (you wouldn't be able to unregister them later).");
#endif
                actions.Add(handler);
        }

        /// <summary>
        /// Removes a listener from this event
        /// </summary>
        /// <param name="handler">Method to be unregistered from event</param>
        public void Unregister(Action a)
        {
            actions.Remove(a);
        }

        /// <summary>
        /// Unregisters all callbacks.
        /// </summary>
        public void UnregisterAll()
        {
            actions.Clear();
        }

        /// <summary>
        /// Trigger all callbacks.
        /// </summary>
        public void Invoke()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                try
                {
                    if (actions[i] != null)
                        actions[i]();
                    else
                        Debug.LogAssertion("Null listener on Event", this);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }

    public class Event<T> : ScriptableObject
    {
        [SerializeField]
        private readonly List<Action<T>> actions = new List<Action<T>>();

        /// <summary>
        /// How many actions registered.
        /// </summary>
        public int Count
        {
            get { return actions.Count; }
        }

        /// <summary>
        /// Adds a listener to this Event
        /// </summary>
        /// <param name="handler">Method to be called when event is fired</param>
        public void Register(Action<T> handler)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Assert(handler.Method.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), inherit: false).Length == 0,
                "Adding anonymous delegates as Signal callbacks is not supported (you wouldn't be able to unregister them later).");
#endif
            actions.Add(handler);
        }

        /// <summary>
        /// Removes a listener from this event
        /// </summary>
        /// <param name="handler">Method to be unregistered from event</param>
        public void Unregister(Action<T> a)
        {
            actions.Remove(a);
        }

        /// <summary>
        /// Unregisters all callbacks.
        /// </summary>
        public void UnregisterAll()
        {
            actions.Clear();
        }

        /// <summary>
        /// Trigger all callbacks.
        /// </summary>
        public void Invoke(T arg0)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                try
                {
                    if (actions[i] != null)
                        actions[i](arg0);
                    else
                        Debug.LogAssertion("Null listener on Event", this);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}