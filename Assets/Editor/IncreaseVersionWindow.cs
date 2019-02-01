using UnityEditor;
using UnityEngine;

namespace LUT
{
	public sealed class IncreaseVersionWindow : EditorWindow
	{
		public enum SemanticVersionType
		{
			Major,
			Minor,
			Patch,
			Dev
		}

		private const string windowName = "Increase Version";
		private static char separator = '.';
		private static SemanticVersionType semanticVersionType;

		private static IncreaseVersionWindow instance;

		[MenuItem("Tools/Increase Version %&v")]
		public static void Open()
		{
			if (!instance)
			{
				instance = CreateInstance<IncreaseVersionWindow>();
				instance.name = windowName;
				//instance.position = new Rect(Screen.width / 2f, Screen.height / 2f, 300, EditorGUIUtility.singleLineHeight * 7);
				instance.ShowUtility();
			}
		}


		private void OnGUI()
		{

			semanticVersionType = (SemanticVersionType)EditorGUILayout.EnumPopup("Version Type", semanticVersionType);
			separator = EditorGUILayout.TextField("Separator", separator.ToString())[0];


			//GUILayout.Space();
			if (GUILayout.Button("Update"))
			{
				Increase();
				Close();
			}

		}

		public static void Increase()
		{
			IncreaseVersion(semanticVersionType, separator);
			IncreaseAndroidBundleVersion();
			IncreaseiOSBuildNumber();
			IncreaseMacBuildNumber();
		}

		public static void IncreaseVersion(SemanticVersionType increaseVersionType, char separator)
		{
			string[] parts = PlayerSettings.bundleVersion.Split(separator);
			int version = 1;
			int index = (int)increaseVersionType;

			if (index >= parts.Length)
			{
				string[] versions = new string[(index + 1) - parts.Length];
				for (int i = 0; i < versions.Length; i++)
				{
					versions[i] = "0";
				}
				ArrayUtility.AddRange(ref parts, versions);
			}
			else if (int.TryParse(parts[index], out version))
			{
				version += 1;
			}

			for (int i = index + 1; i < parts.Length;)
			{
				ArrayUtility.RemoveAt(ref parts, i);
			}

			parts[index] = version.ToString();
			PlayerSettings.bundleVersion = string.Join(separator.ToString(), parts);
		}


		public static void IncreaseAndroidBundleVersion()
		{
			PlayerSettings.Android.bundleVersionCode++;
		}

		public static void IncreaseiOSBuildNumber()
		{
			if (int.TryParse(PlayerSettings.iOS.buildNumber, out int buildNumber))
			{
				buildNumber++;
				PlayerSettings.iOS.buildNumber = buildNumber.ToString();
			}
		}


		public static void IncreaseMacBuildNumber()
		{
			if (int.TryParse(PlayerSettings.macOS.buildNumber, out int buildNumber))
			{
				buildNumber++;
				PlayerSettings.macOS.buildNumber = buildNumber.ToString();
			}
		}
	}
}
