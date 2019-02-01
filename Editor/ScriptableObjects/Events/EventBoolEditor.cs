using UnityEditor;
using UnityEngine;

namespace LUT.Events.Primitives
{
	[CustomEditor(typeof(EventBool))]
	public sealed class EventBoolEditor : EventObjectTEditor<EventBool, bool>
	{
		private EventBool internalEvent;
		private SerializedProperty sp_valueToInvoke;

		private void OnEnable()
		{
			internalEvent = (EventBool)target;
			sp_valueToInvoke = serializedObject.FindProperty("valueToInvokeWith");
		}
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			base.OnInspectorGUI();

			if (GUILayout.Button("Trigger event"))
			{
				internalEvent.Invoke(sp_valueToInvoke.boolValue);
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}
