using System.IO;
using UnityEditor;
using UnityEngine;

namespace LUT
{
	/// <summary>
	/// Editor extension that adds a tool to automagically generate boilerplate custom inspector code
	/// developed by @LotteMakesStuff
	/// available @ https://gist.github.com/LotteMakesStuff/cb63e4e25e5dfdda19a95380e9c03436
	/// </summary>

	public static class CustomInspectorCreator
	{
		[MenuItem("Assets/Create/Custom/Inspector", priority = 83)]
		static void CreateInsptorEditorClass()
		{
			foreach (var script in Selection.objects)
			{
				BuildEditorFile(script);
			}

			AssetDatabase.Refresh();
		}

		[MenuItem("Assets/Create/Custom/Inspector", priority = 83, validate = true)]
		static bool ValidateCreateInsptorEditorClass()
		{
			foreach (var script in Selection.objects)
			{
				string path = AssetDatabase.GetAssetPath(script);

				if (script.GetType() != typeof(MonoScript))
				{
					return false;
				}

				if (!path.EndsWith(".cs"))
				{
					return false;
				}

				if (path.Contains("Editor"))
				{
					return false;
				}
			}

			return true;
		}

		static void BuildEditorFile(Object obj)
		{
			MonoScript monoScript = obj as MonoScript;
			if (monoScript == null)
			{
				Debug.Log("ERROR: Cannot generate a custom inspector, Selected script was not a MonoBehavior");
				return;
			}

			string assetPath = AssetDatabase.GetAssetPath(obj);
			var filename = Path.GetFileNameWithoutExtension(assetPath);
			string script = "";
			string scriptNamespace = monoScript.GetClass().Namespace;

			if (scriptNamespace == null)
			{
				// No namespace, use the default template
				script = string.Format(template, filename);
			}
			else
			{
				script = string.Format(namespaceTemplate, filename, scriptNamespace);
			}

			// make sure a editor folder exists for us to put this script into...       
			var editorFolder = Path.GetDirectoryName(assetPath) + "/Editor";

			if (!Directory.Exists(editorFolder))
			{
				Directory.CreateDirectory(editorFolder);
			}

			if (File.Exists(editorFolder + "/" + filename + "Inspector.cs"))
			{
				Debug.Log("ERROR: " + filename + "Inspector.cs already exists.");
				return;
			}

			// finally write out the new editor~
			File.WriteAllText(editorFolder + "/" + filename + "Inspector.cs", script);
		}

		#region Templates
		static readonly string template = @"using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof({0}))]
//[CanEditMultipleObjects]
public class {0}Inspector : Editor
{{
    void OnEnable()
    {{
        // TODO: find properties we want to work with
        //serializedObject.FindProperty();
    }}

    public override void OnInspectorGUI()
    {{
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        // TODO: Draw UI here
        //EditorGUILayout.PropertyField();
        DrawDefaultInspector();

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }}
}}
";

		static readonly string namespaceTemplate = @"using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace {1}
{{
    [CustomEditor(typeof({0}))]
    //[CanEditMultipleObjects]
    public class {0}Inspector : Editor
    {{
        void OnEnable()
        {{
            // TODO: find properties we want to work with
            //serializedObject.FindProperty();
        }}

        public override void OnInspectorGUI()
        {{
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

            // TODO: Draw UI here
            //EditorGUILayout.PropertyField();
            DrawDefaultInspector();

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }}
    }}
}}
";
		#endregion
	}
}
