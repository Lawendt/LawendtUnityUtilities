using UnityEditor;
using UnityEngine;

/// <summary>
/// Adds a draw script function to the Editor class.
/// Developed by @la_wendt. 
/// Available @ https://github.com/Lawendt/UnityLawUtilities
/// </summary>
public static class ExtendEditor
{

	public static void DrawScript(this Editor editor)
	{
		EditorGUI.BeginDisabledGroup(true);
		SerializedProperty prop = editor.serializedObject.FindProperty("m_Script");
		EditorGUILayout.PropertyField(prop, true, new GUILayoutOption[0]);
		EditorGUI.EndDisabledGroup();

	}

}
