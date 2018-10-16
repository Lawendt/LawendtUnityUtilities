using System;
using System.Collections.Generic;
using UnityEngine;

namespace LUT.Events
{
	/// <summary>
	/// Serialized event
	/// </summary>
	[CreateAssetMenu(fileName = "Event", menuName = "Event/Default", order = 50)]
	public class EventObject : ScriptableObject
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
			// Iterate on the handler list backwards
			// so that if any of the handlers remove
			// themselves from the list, it will not
			// crash the loop
			for (int i = actions.Count - 1; i >= 0; i -= 1)
			{
				try
				{
					if (actions[i] != null)
					{
						actions[i]();
					}
					else
					{
						Debug.LogAssertion("Null listener on Event", this);
					}
				}
				catch (Exception e)
				{
					Debug.LogException(e);
				}
			}
		}
	}

	public class EventObject<T> : ScriptableObject
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
		public void Invoke(T arg0 = default(T))
		{
			// Iterate on the handler list backwards
			// so that if any of the handlers remove
			// themselves from the list, it will not
			// crash the loop
			for (int i = actions.Count - 1; i >= 0; i -= 1)
			{
				try
				{
					if (actions[i] != null)
					{
						actions[i](arg0);
					}
					else
					{
						Debug.LogAssertion("Null listener on Event", this);
					}
				}
				catch (Exception e)
				{
					Debug.LogException(e);
				}
			}
		}
	}
}