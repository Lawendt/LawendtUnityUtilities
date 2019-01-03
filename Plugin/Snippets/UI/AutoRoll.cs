using UnityEngine;
using UnityEngine.UI;

namespace LUT.Snippets.UI
{

	/// <summary>
	/// Auto rolls an scroll rect with the desired speed.
	/// Developed by Lawendt. 
	/// Available @ https://github.com/Lawendt/UnityLawUtilities
	/// </summary>
	public class AutoRoll : MonoBehaviour
	{
		[Tooltip("Speed in unit/sec")]
		public float horizontalSpeed;
		[Tooltip("Speed in unit/sec")]
		public float verticalSpeed;
		[AutoFind(typeof(ScrollRect), true)]
		public ScrollRect scrollRect;
		public bool active = true;
		public bool autoDeactive;
		public bool deactivedWithTouch;
		public bool pong;

		// Use this for initialization
		void Start()
		{

		}

		private bool temp = false;
		float lastHorizontal = -42, lastVertical = -42;
		// Update is called once per frame
		void Update()
		{
			if (active)
			{
				if (deactivedWithTouch)
				{
					if (lastHorizontal != -42)
					{
						if (scrollRect.horizontalNormalizedPosition != lastHorizontal)
						{
							active = false;
						}

						if (scrollRect.verticalNormalizedPosition != lastVertical)
						{
							active = false;
						}
					}

				}
				scrollRect.horizontalNormalizedPosition -= horizontalSpeed * Time.deltaTime;
				scrollRect.verticalNormalizedPosition -= verticalSpeed * Time.deltaTime;

				lastHorizontal = scrollRect.horizontalNormalizedPosition;
				lastVertical = scrollRect.verticalNormalizedPosition;

				if (autoDeactive || pong)
				{
					temp = false;
					if (horizontalSpeed > 0)
					{
						if (scrollRect.horizontalNormalizedPosition <= 0)
						{
							temp = true;
						}
					}
					else if (horizontalSpeed < 0)
					{
						if (scrollRect.horizontalNormalizedPosition >= 1)
						{
							temp = true;
						}
					}
					else
					{
						temp = true;
					}

					if (verticalSpeed > 0)
					{
						if (scrollRect.verticalNormalizedPosition < 0)
						{
							temp = true;
						}
						else
						{
							temp = false;
						}
					}
					else if (verticalSpeed < 0)
					{
						if (scrollRect.verticalNormalizedPosition > 1)
						{
							temp = true;
						}
						else
						{
							temp = false;
						}
					}
					else
					{
						temp = true;
					}
					if (temp)
					{
						if (autoDeactive)
						{
							active = false;
						}

						if (pong)
						{
							horizontalSpeed *= -1;
							verticalSpeed *= -1;
						}
					}
				}

			}

		}
	}
}
