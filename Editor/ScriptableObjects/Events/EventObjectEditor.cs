using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LUT.Events
{
	[CustomEditor(typeof(EventObject))]
	//[CanEditMultipleObjects]
	public class EventObjectEditor : Editor
	{
		private EventObject eTarget;

		private void OnEnable()
		{
			// TODO: find properties we want to work with
			//serializedObject.FindProperty();
		}

		private void OnDisable()
		{
		}

		public override void OnInspectorGUI()
		{
			eTarget = (EventObject)target;
			// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
			serializedObject.Update();


			DrawDefaultInspector();


			if (GUILayout.Button("Trigger event"))
			{
				eTarget.Invoke();
			}


			EditorGUILayout.LabelField(string.Format("There are currently {0} listeners registered.", eTarget.Count));

			var _objAction = typeof(EventObject).GetField("actions", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(eTarget);

			EditorGUILayout.Space();
			EditorGUI.indentLevel++;
			List<Action> actions = (List<Action>)_objAction;
			int nullListenersAction = 0;
			foreach (Action action in actions)
			{
				if (action == null)
				{
					nullListenersAction++;
				}
				else
				{
					Type type = action.Target.GetType();
					if (action.Target is MonoBehaviour)
					{
						MonoBehaviour mono = action.Target as MonoBehaviour;
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField(string.Format("{0} -> {1}{2}", mono.gameObject.name, type.ToString(), action.GetMethodInfo().Name));
						if (GUILayout.Button("Select"))
						{
							Selection.activeGameObject = mono.gameObject;
						}
						EditorGUILayout.EndHorizontal();

					}
					else
					{
						EditorGUILayout.LabelField(string.Format("{0}{1}", type.ToString(), action.GetMethodInfo().Name));
					}

				}
			}
			EditorGUI.indentLevel--;

			if (nullListenersAction != 0)
			{
				EditorGUILayout.HelpBox("There are " + nullListenersAction + " null listeners on this event.", MessageType.Warning);
			}


			// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
			serializedObject.ApplyModifiedProperties();
		}

	}



	public class EventObjectTEditor<EventObjectType,T> : Editor where EventObjectType : EventObject<T>
	{
		public override void OnInspectorGUI()
		{
			EventObjectType eTarget = (EventObjectType)target;
			// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
			serializedObject.Update();

			DrawDefaultInspector();

			EditorGUILayout.LabelField(string.Format("There are currently {0} listeners registered.", eTarget.Count));

			var _objAction = typeof(EventObjectType).BaseType.GetField("actions", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(target);

			EditorGUILayout.Space();
			EditorGUI.indentLevel++;
			List<Action<T>> actions = (List<Action<T>>)_objAction;
			int nullListenersAction = 0;
			foreach (var action in actions)
			{
				if (action == null)
				{
					nullListenersAction++;
				}
				else
				{
					Type type = action.Target.GetType();
					if (action.Target is MonoBehaviour)
					{
						MonoBehaviour mono = action.Target as MonoBehaviour;
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField(string.Format("{0} -> {1}{2}", mono.gameObject.name, type.ToString(), action.GetMethodInfo().Name));
						if (GUILayout.Button("Select"))
						{
							Selection.activeGameObject = mono.gameObject;
						}
						EditorGUILayout.EndHorizontal();

					}
					else
					{
						EditorGUILayout.LabelField(string.Format("{0}{1}", type.ToString(), action.GetMethodInfo().Name));
					}

				}
			}
			EditorGUI.indentLevel--;

			if (nullListenersAction != 0)
			{
				EditorGUILayout.HelpBox("There are " + nullListenersAction + " null listeners on this event.", MessageType.Warning);
			}


			// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
			serializedObject.ApplyModifiedProperties();
		}
	}
}