using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LUT
{
	public static class EasyButtonsEditorExtensions
	{
		public static void DrawButtons(this Editor editor)
		{
			// Loop through all methods with no parameters
			var methods = editor.target.GetType()
				.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				.Where(m => m.GetParameters().Length == 0);
			foreach (var method in methods)
			{
				// Get the ButtonAttribute on the method (if any)
				var attribute = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));

				if (attribute != null)
				{
					// Determine whether the button should be enabled based on its mode
					GUI.enabled = attribute.Mode == ButtonMode.AlwaysEnabled
						|| (EditorApplication.isPlaying ? attribute.Mode == ButtonMode.EnabledInPlayMode : attribute.Mode == ButtonMode.DisabledInPlayMode);

					// Draw a button which invokes the method
					var buttonName = string.IsNullOrEmpty(attribute.Name) ? ObjectNames.NicifyVariableName(method.Name) : attribute.Name;
					if (GUILayout.Button(buttonName))
					{
						foreach (var t in editor.targets)
						{
							method.Invoke(t, null);
						}
					}

					GUI.enabled = true;
				}
			}
		}
	}
}
