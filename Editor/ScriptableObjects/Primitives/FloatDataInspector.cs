using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace LUT.Primitive
{
    [CustomEditor(typeof(FloatData))]
    //[CanEditMultipleObjects]
    public class FloatDataInspector : Editor
    {

        void OnEnable()
        {
            // TODO: find properties we want to work with
            //serializedObject.FindProperty();
        }

        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

            SerializedProperty _lock = serializedObject.FindProperty("_lock");
            SerializedProperty _value = serializedObject.FindProperty("_value");

            EditorGUI.BeginDisabledGroup(_lock.boolValue);
            EditorGUILayout.PropertyField(_value);
            EditorGUI.EndDisabledGroup();

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }
    }
}
