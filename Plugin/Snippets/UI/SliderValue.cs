using UnityEngine;
using UnityEngine.UI;

namespace LUT.Snippets.UI
{
	public class SliderValue : MonoBehaviour
	{

		public Slider slider;
		Text text;

		private void Start()
		{
			text = GetComponent<Text>();
		}

		void Update()
		{
			text.text = slider.value.ToString("0.00");
		}
	}
}
