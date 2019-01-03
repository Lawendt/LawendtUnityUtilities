using UnityEditor;

namespace LUT.Primitive
{
	[CustomEditor(typeof(FloatData))]
	public class FloatDataInspector : Editor
	{
		SerializedProperty _value;

		void OnEnable()
		{
			_value = serializedObject.FindProperty("_value");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.PropertyField(_value);

			serializedObject.ApplyModifiedProperties();
		}
	}
}
