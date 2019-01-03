using System.IO;
using UnityEditor;
using UnityEngine;

namespace LUT
{

	/// <summary>
	/// Editor extension that adds a unity event of a signal
	/// developed by @La_wendt
	/// based on @ https://gist.github.com/LotteMakesStuff/cb63e4e25e5dfdda19a95380e9c03436
	/// </summary>

	public static class CustomUnityEventSignal
	{
		[MenuItem("Assets/Create/Custom/Unity Event Signal", priority = 83)]
		static void CreateInsptorEditorClass()
		{
			foreach (var script in Selection.objects)
			{
				BuildEditorFile(script);
			}

			AssetDatabase.Refresh();
		}

		[MenuItem("Assets/Create/Custom/Unity Event Signal", priority = 83, validate = true)]
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
				Debug.Log("ERROR: Cannot generate a custom inspector, Selected script was not a Asignal");
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
			var folder = Path.GetDirectoryName(assetPath) + "/";

			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			if (File.Exists(folder + "/" + "UnityEvent" + filename + ".cs"))
			{
				Debug.Log("ERROR: " + "UnityEvent" + filename + ".cs already exists.");
				return;
			}

			// finally write out the new editor~
			File.WriteAllText(folder + "/" + "UnityEvent" + filename + ".cs", script);
		}

		#region Templates
		static readonly string template = @"using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnityEvent{0} : UnityEventSignal<{0}>
{{

}}
";

		static readonly string namespaceTemplate = @"using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace {1}
{{
public class UnityEvent{0} : UnityEventSignal<{0}>
{{

}}
}}
";
		#endregion
	}
}
