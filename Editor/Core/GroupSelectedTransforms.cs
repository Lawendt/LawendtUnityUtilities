using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LUT
{
	/// <summary>
	/// Adds a `ctrl + shift + g` command that groups the selected game objects inside a empty game object 
	/// Developed by Daniel Mullins.
	/// </summary>
	public class GroupUnderParent
	{
		private const string SHORTCUT_NAME = "Shortcuts/Group Under Parent %#g";

		[MenuItem(SHORTCUT_NAME)]
		private static void GroupTransforms()
		{
			var parent = new GameObject("New Group");
			Undo.RegisterCreatedObjectUndo(parent, "Created New Group");

			var selectedTransforms = GetSelectedTransforms();

			Vector2 sumPosition = Vector2.zero;
			foreach (Transform t in selectedTransforms)
			{
				sumPosition += (Vector2)t.transform.position;
			}
			parent.transform.position = sumPosition / selectedTransforms.Count;

			foreach (Transform t in selectedTransforms)
			{
				Undo.SetTransformParent(t, parent.transform, "Moved '" + t.name + "' Into New Group");
			}

			Undo.FlushUndoRecordObjects();
		}

		[MenuItem(SHORTCUT_NAME, true)]
		private static bool Validate()
		{
			return GetSelectedTransforms().Count > 0;
		}

		private static List<Transform> GetSelectedTransforms()
		{
			return new List<Transform>(Selection.GetTransforms(SelectionMode.TopLevel));
		}
	}
}