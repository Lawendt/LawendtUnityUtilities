using UnityEditor;
using UnityEngine;

namespace LUT.Events.Primitives
{
	[CustomEditor(typeof(EventInt))]
	public sealed class EventIntEditor : Editor
	{
		EventInt internalEventInt;
		int valueToInvokeWith = 0;
		private void OnEnable()
		{
			internalEventInt = (EventInt)target;
		}
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			base.OnInspectorGUI();


			valueToInvokeWith = EditorGUILayout.IntField("Trigger with:", valueToInvokeWith);

			if (GUILayout.Button("Trigger event"))
			{
				internalEventInt.Invoke(valueToInvokeWith);
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}
