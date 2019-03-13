using UnityEngine;
using UnityEngine.UI;

namespace LUT.Snippets.UI
{
	/// <summary>
	/// Linearly changes the hue over time.
	/// Developed by Lawendt. 
	/// Available @ https://github.com/Lawendt/UnityLawUtilities
	/// </summary>
	public class ColorfulScript : MonoBehaviour
	{
		[AutoFind(typeof(Graphic), true)]
		public Graphic t;
		public float secondsToAll;
		private float currentHue;
		private float currentSat;
		private float currentValue;

		public Color baseColor;
		void Start()
		{
			Color.RGBToHSV(baseColor, out currentHue, out currentSat, out currentValue);
		}


		void Update()
		{
			currentHue += 1 / secondsToAll * Time.deltaTime;
			currentHue %= 1;
			t.color = Color.HSVToRGB(currentHue, currentSat, currentValue);
		}
	}
}
