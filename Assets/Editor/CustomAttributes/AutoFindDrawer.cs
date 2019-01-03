using UnityEditor;
using UnityEngine;

namespace LUT
{
	[CustomPropertyDrawer(typeof(AutoFind))]
	public class AutoFindDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			int indent = EditorGUI.indentLevel;
			AutoFind autoFind = attribute as AutoFind;
			Transform targetTransform = ((MonoBehaviour)property.serializedObject.targetObject).transform;
			EditorGUI.indentLevel = 0;
			Rect propertyRect = new Rect(position.x, position.y, position.width * 0.85f, position.height);
			Rect buttonRect = new Rect(position.x + position.width * 0.85f, position.y, position.width * 0.15f, position.height);
			EditorGUI.PropertyField(propertyRect, property, label);
			if (property.objectReferenceValue == null)
			{
				if (GUI.Button(buttonRect, new GUIContent("Find", "Find " + autoFind.objectType.Name + " in hierarchy"), EditorStyles.miniButton))
				{
					Component foundComp = autoFind.searchInChildren ? targetTransform.GetComponentInChildren(autoFind.objectType, true) : targetTransform.GetComponent(autoFind.objectType);
					if (foundComp != null)
					{
						property.objectReferenceValue = foundComp;
					}
				}
			}
			else
			{
				Rect MiniButtonLeft = buttonRect;
				MiniButtonLeft.width = buttonRect.width * 0.5f;
				Rect MiniButtonRight = MiniButtonLeft;
				MiniButtonRight.x = MiniButtonLeft.x + MiniButtonLeft.width;
				if (GUI.Button(MiniButtonLeft, new GUIContent("↺", "Cycle through " + autoFind.objectType.Name), EditorStyles.miniButtonLeft))
				{
					Component[] compList = autoFind.searchInChildren ? targetTransform.GetComponentsInChildren(autoFind.objectType, true) : targetTransform.GetComponents(autoFind.objectType);
					int index = 0;
					for (int a = 0; a < compList.Length; a++)
					{
						if (compList[a] == property.objectReferenceValue)
						{
							index = a;
							break;
						}
					}
					if (index == compList.Length - 1)
					{
						index = -1;
					}
					index++;
					property.objectReferenceValue = compList[index];
				}
				if (GUI.Button(MiniButtonRight, new GUIContent("✖", "Set to null"), EditorStyles.miniButtonRight))
				{
					property.objectReferenceValue = null;
				}

			}
			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label);
		}

	}
}
