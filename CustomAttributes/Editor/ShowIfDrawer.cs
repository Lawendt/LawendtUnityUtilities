// NOTE put in a Editor folder

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomPropertyDrawer(typeof(ShowIf))]
public class ShowIfDrawer : PropertyDrawer
{
    bool hideIt = true;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var hideAttribute = attribute as ShowIf;

        if (CheckIfShow(property))
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
    override public float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!CheckIfShow(property))
            return 0;
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public bool CheckIfShow(SerializedProperty property)
    {
        var hideAttribute = attribute as ShowIf;

        if (!string.IsNullOrEmpty(hideAttribute.ValidateMethod))
        {
            var t = property.serializedObject.targetObject.GetType();
            var m = t.GetMethod(hideAttribute.ValidateMethod, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (m != null)
            {
                 return (bool)m.Invoke(property.serializedObject.targetObject, new[] { hideAttribute.Value });
            }
            else
            {
                Debug.LogError("Invalid Validate function: " + hideAttribute.ValidateMethod, property.serializedObject.targetObject);
            }
        }
        return true;
    }




}
