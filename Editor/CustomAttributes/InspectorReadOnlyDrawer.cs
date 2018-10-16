using UnityEditor;
using UnityEngine;

namespace LUT
{
	[CustomPropertyDrawer(typeof(InspectorReadOnly))]
	public class InspectorReadOnlyDrawer : PropertyDrawer
	{
		override public float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}

		override public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			GUI.enabled = false;
			EditorGUI.PropertyField(position, property, label, true);
			GUI.enabled = true;
		}

	}
}