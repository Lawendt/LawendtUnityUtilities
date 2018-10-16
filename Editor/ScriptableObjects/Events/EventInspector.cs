using UnityEditor;
using UnityEngine;

namespace LUT.Events
{
	[CustomEditor(typeof(EventObject))]
	//[CanEditMultipleObjects]
	public class EventInspector : Editor
	{
		EventObject eTarget;
		void OnEnable()
		{
			// TODO: find properties we want to work with
			//serializedObject.FindProperty();
		}

		public override void OnInspectorGUI()
		{
			eTarget = (EventObject)target;
			// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
			serializedObject.Update();


			DrawDefaultInspector();

			// TODO: Draw UI here
			EditorGUILayout.HelpBox("There are currently " + eTarget.Count + " listeners registered.", MessageType.Info);

			if (GUILayout.Button("Trigger event"))
			{
				eTarget.Invoke();
			}

			//var _objAction = typeof(Event).GetField("actions", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(eTarget);

			//List<Action> actions = (List<Action>)_objAction;
			//int c = 0;
			//foreach(Action a in actions)
			//{
			//    if(a == null)
			//    {
			//        c++;
			//    }
			//}
			//if(c!= 0)
			//{
			//    EditorGUILayout.HelpBox("There are " + c + " null listeners on this event.", MessageType.Warning);
			//}

			// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
			serializedObject.ApplyModifiedProperties();
		}
	}
}
