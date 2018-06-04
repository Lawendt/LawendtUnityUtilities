using UnityEngine;

namespace LUT.Snippets
{

    public class ScreenshotUtil : Singleton<ScreenshotUtil>
    {
        [SerializeField]
        private int _screenshotNumber;
        [SearchableEnum.SearchableEnum]
        public KeyCode keyCode = KeyCode.P;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                Capture();
            }
        }

        [EasyButtons.Button()]
        void Capture()
        {
            string path = Application.persistentDataPath + "/screenshot_" + _screenshotNumber + ".png";
            ScreenCapture.CaptureScreenshot(path);
            Debug.Log("screenshot was taken and saved to " + path);
            _screenshotNumber++;

        }
    }
}
