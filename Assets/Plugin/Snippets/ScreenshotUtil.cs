using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LUT.Snippets
{
	public class ScreenshotUtil : Singleton<ScreenshotUtil>
	{
		[SerializeField]
		private int _screenshotNumber;

		[SearchableEnum]
		public KeyCode keyCode = KeyCode.P;

		[Header("Path")]
		[SerializeField]
		[ContextMenuItem("OpenPath", "OpenPath")]
		private string _path = "Screenshot" + Path.AltDirectorySeparatorChar;
		[SerializeField]
		private bool _usePersistentDataPath = false;
		[SerializeField]
		private bool _appendResolution = false;
		[SerializeField]
		private bool _appendCurrentSceneName = false;

		private void Update()
		{
			if (Input.GetKeyDown(keyCode))
			{
				Capture();
			}
		}

		[Button(ButtonMode.EnabledInPlayMode)]
		public void Capture()
		{
			GetPath(out string path);

			ScreenCapture.CaptureScreenshot(path);
			Debug.Log(string.Format("{0} saved at", Path.GetFileName(path), Path.GetDirectoryName(path)));
			_screenshotNumber++;

		}

#if UNITY_EDITOR
		public Vector2 GetMainGameViewSize()
		{
			System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
			System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
			object Res = GetSizeOfMainGameView.Invoke(null, null);
			return (Vector2)Res;
		}
#endif

		public void GetPath(out string path)
		{
			path = string.Empty;
			if (_usePersistentDataPath)
			{
				path = Application.persistentDataPath + "/";
			}
			else
			{
				path = Application.dataPath + "/";
				path = path.Remove(path.IndexOf("Assets/"));
			}

			path += _path;

			string dir = Path.GetDirectoryName(path);
			if (Directory.Exists(dir) == false)
			{
				Directory.CreateDirectory(dir);
			}

			if (_appendResolution)
			{
				string resolution = " ";
#if UNITY_EDITOR
				resolution = GetMainGameViewSize().ToString();
				resolution = resolution.Replace("(", "");
				resolution = resolution.Replace(")", "");
				resolution = resolution.Replace(" ", "");
				var res = resolution.Split(',');
				resolution = string.Format("{0}x{1}", res[0], res[2]);
#else
				resolution = Screen.currentResolution.ToString();
				resolution = resolution.Replace(" ", "");
                resolution = resolution.Remove(resolution.IndexOf("@"));
#endif
				if (!string.IsNullOrEmpty(Path.GetFileName(path)))
				{
					path += "_";
				}
				path += resolution;
			}

			if (_appendCurrentSceneName)
			{
				path += string.Format("_{0}", SceneManager.GetActiveScene().name);
			}

			path += string.Format("_{0}.png", _screenshotNumber);
		}

		private string GetDirectory(string path)
		{
			string dir = Path.GetDirectoryName(path);
			if (Directory.Exists(dir) == false)
			{
				Directory.CreateDirectory(dir);
			}
			return dir;
		}

#if UNITY_EDITOR
		[Button()]
		public void OpenPath()
		{
			GetPath(out string path);
			string dir = GetDirectory(path);
			UnityEditor.EditorUtility.RevealInFinder(dir);
		}
#endif
	}
}