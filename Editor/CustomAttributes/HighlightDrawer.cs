using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LUT
{
	[CustomPropertyDrawer(typeof(HighlightAttribute))]
	public class HighlightDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var highlightAttribute = attribute as HighlightAttribute;

			bool doHighlight = true;

			// do we have a validation method 
			if (!string.IsNullOrEmpty(highlightAttribute.ValidateMethod))
			{
				var t = property.serializedObject.targetObject.GetType();
				var m = t.GetMethod(highlightAttribute.ValidateMethod, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

				if (m != null)
				{
					doHighlight = (bool)m.Invoke(property.serializedObject.targetObject, new[] { highlightAttribute.Value });
				}
				else
				{
					Debug.LogError("Invalid Validate function: " + highlightAttribute.ValidateMethod, property.serializedObject.targetObject);
				}
			}

			if (doHighlight)
			{
				// get the highlight color
				var color = GetColor(highlightAttribute.Color);

				// create a ractangle to draw the highlight to, slightly larger than our property
				var padding = EditorGUIUtility.standardVerticalSpacing;
				var highlightRect = new Rect(position.x - padding, position.y - padding,
					position.width + (padding * 2), position.height + (padding * 2));

				// draw the highlight first
				EditorGUI.DrawRect(highlightRect, color);

				// make sure the propertys text is dark and easy to read over the bright highlight
				var cc = GUI.contentColor;
				GUI.contentColor = Color.black;

				// draw the property ontop of the highlight
				EditorGUI.PropertyField(position, property, label);

				GUI.contentColor = cc;
			}
			else
			{
				EditorGUI.PropertyField(position, property, label);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}

		private Color GetColor(HighlightColor color)
		{
			switch (color)
			{
				case HighlightColor.Red:
					return new Color32(255, 0, 63, 255);
				case HighlightColor.Pink:
					return new Color32(255, 66, 160, 255);
				case HighlightColor.Orange:
					return new Color32(255, 128, 0, 255);
				case HighlightColor.Yellow:
					return new Color32(255, 211, 0, 255);
				case HighlightColor.Green:
					return new Color32(102, 255, 0, 255);
				case HighlightColor.Blue:
					return new Color32(0, 135, 189, 255);
				case HighlightColor.Violet:
					return new Color32(127, 0, 255, 255);
				default:
					return Color.white;
			}
		}
	}
}
