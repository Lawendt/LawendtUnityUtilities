
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LUT
{

	[CustomPropertyDrawer(typeof(ShowIf))]
	public class ShowIfDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (CheckIfShow(property))
			{
				EditorGUI.PropertyField(position, property, label);
			}
		}
		override public float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!CheckIfShow(property))
			{
				return 0;
			}

			return EditorGUI.GetPropertyHeight(property, label, true);
		}

		public bool CheckIfShow(SerializedProperty property)
		{
			var hideAttribute = attribute as ShowIf;

			if (!string.IsNullOrEmpty(hideAttribute.ValidateMethod))
			{
				var t = property.serializedObject.targetObject.GetType();
				if (hideAttribute.Type == ShowIf.ShowIfType.Method)
				{
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
				else
				{
					var f = t.GetField(hideAttribute.ValidateMethod, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

					if (f != null)
					{
						if (hideAttribute.Value != null)
						{
							if (hideAttribute.Type == ShowIf.ShowIfType.FieldNotEquals)
							{
								return !f.GetValue(property.serializedObject.targetObject).Equals(hideAttribute.Value);
							}
							else if (hideAttribute.Type == ShowIf.ShowIfType.FieldEquals)
							{
								return f.GetValue(property.serializedObject.targetObject).Equals(hideAttribute.Value);
							}
						}
						else
						{
							if (f.FieldType == typeof(bool))
							{
								if (hideAttribute.Type == ShowIf.ShowIfType.FieldNotEquals)
								{
									return !f.GetValue(property.serializedObject.targetObject).Equals(true);
								}
								else if (hideAttribute.Type == ShowIf.ShowIfType.FieldEquals)
								{
									return f.GetValue(property.serializedObject.targetObject).Equals(true);
								}
							}
							else
							{
								Debug.LogError(hideAttribute.ValidateMethod + " must be bool if no object is given. But is " + f.FieldType, property.serializedObject.targetObject);
							}
						}
					}
					else
					{
						Debug.LogError("Invalid field: " + hideAttribute.ValidateMethod, property.serializedObject.targetObject);
					}
				}
			}
			return true;
		}
	}
}
