using UnityEditor;
using UnityEngine;

namespace LUT.Events.Primitives
{
	[CustomEditor(typeof(EventInt))]
	public sealed class EventIntEditor : EventObjectTEditor<EventInt, int>
	{
		EventInt internalEventInt;
		private SerializedProperty _valueToInvokeWith;

		private void OnEnable()
		{
			internalEventInt = (EventInt)target;
			_valueToInvokeWith = serializedObject.FindProperty("valueToInvokeWith");

		}
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			base.OnInspectorGUI();


			if (GUILayout.Button("Trigger event"))
			{
				internalEventInt.Invoke(_valueToInvokeWith.intValue);
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}
