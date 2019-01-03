using UnityEditor;
using UnityEngine;

namespace LUT.Events.Primitives
{
	[CustomEditor(typeof(EventFloat))]
	public sealed class EventFloatEditor : Editor
	{
		private EventFloat internalEvent;
		private SerializedProperty sp_valueToInvoke;

		private void OnEnable()
		{
			internalEvent = (EventFloat)target;
			sp_valueToInvoke = serializedObject.FindProperty("valueToInvokeWith");
		}
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			base.OnInspectorGUI();

			if (GUILayout.Button("Trigger event"))
			{
				internalEvent.Invoke(sp_valueToInvoke.floatValue);
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}
