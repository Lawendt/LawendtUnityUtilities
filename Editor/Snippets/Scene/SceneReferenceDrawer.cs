using UnityEditor;
using UnityEngine;

namespace LUT.Snippets
{
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferenceDrawer : PropertyDrawer
    {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight* 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();

            SerializedProperty sceneObject = property.FindPropertyRelative("scene");
            SerializedProperty sceneName = property.FindPropertyRelative("sceneName");
            EditorGUI.PropertyField(position, sceneObject);
            position.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(position, sceneName);
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                SceneAsset scene = sceneObject.objectReferenceValue as SceneAsset;
                if (scene)
                {
                    sceneName.stringValue = scene.name;
                }
                else
                {
                    sceneName.stringValue = "null";
                }
            }
            EditorGUI.EndProperty();
        }

    }
}