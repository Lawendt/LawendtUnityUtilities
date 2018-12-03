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
        private string _path = "screenshot_";
        [SerializeField]
        private bool _usePersistentDataPath = false;
        [SerializeField]
        private bool _appendResolution = false;
        [SerializeField]
        private bool _appendCurrentSceneName = false;

        void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                Capture();
            }
        }

        [Button(ButtonMode.EnabledInPlayMode)]
        public void Capture()
        {
            string path;
            GetPath(out path);

            ScreenCapture.CaptureScreenshot(path);
            Debug.Log("screenshot was taken and saved to " + path);
            _screenshotNumber++;

        }

        public void GetPath(out string path)
        {
            path = "";
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
                string resolution = Screen.currentResolution.ToString();
                resolution = resolution.Replace(" ", "");
                resolution = resolution.Remove(resolution.IndexOf("@"));
                path += "_" + resolution;
            }

            if (_appendCurrentSceneName)
            {
                path += "_" + SceneManager.GetActiveScene().name;
            }

            path += "_" + _screenshotNumber + ".png";
        }

        string GetDirectory(string path)
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
            string path = _path;
            GetPath(out path);
            string dir = GetDirectory(path);
            UnityEditor.EditorUtility.RevealInFinder(dir);
        }
#endif
    }
}