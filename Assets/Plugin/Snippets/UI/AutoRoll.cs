using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LUT.Snippets
{

	/// <summary>
	/// Auto rolls an scroll rect with the desired speed.
	/// Developed by Lawendt. 
	/// Available @ https://github.com/Lawendt/UnityLawUtilities
	/// </summary>
	public class AutoRoll : MonoBehaviour, IDragHandler
	{
		[Tooltip("Speed in unit/sec")]
		public float horizontalSpeed = 0.05f;
		[Tooltip("Speed in unit/sec")]
		public float verticalSpeed = 0.05f;

		[AutoFind(typeof(ScrollRect), true)]
		public ScrollRect scrollRect;

		public bool active = true;


		public bool deactivateWithDrag = true;
		[ShowIf("deactivateWithDrag")]
		public bool comeBackAfterDeactivateWithDrag = true;
		[ShowIf("comeBackAfterDeactivateWithDrag")]
		public float waitSeconds = 1;

		public bool pong = false;

		private void Update()
		{
			if (active)
			{
				scrollRect.horizontalNormalizedPosition -= horizontalSpeed * Time.deltaTime;
				scrollRect.verticalNormalizedPosition -= verticalSpeed * Time.deltaTime;

				if (HasEnded())
				{
					if (pong)
					{
						horizontalSpeed *= -1;
						verticalSpeed *= -1;
					}
					else
					{
						Stop();
					}
				}

			}
		}


		private bool HasEnded()
		{
			bool hasEnded;

			if (scrollRect.horizontalNormalizedPosition <= 0 && Mathf.Sign(horizontalSpeed) == 1)
			{
				hasEnded = true;
			}
			else if (scrollRect.horizontalNormalizedPosition >= 1 && Mathf.Sign(horizontalSpeed) == -1)
			{
				hasEnded = true;
			}
			else
			{
				hasEnded = false;
			}

			if (scrollRect.verticalNormalizedPosition <= 0 && Mathf.Sign(verticalSpeed) == 1)
			{
				hasEnded &= true;
			}
			else if (scrollRect.verticalNormalizedPosition >= 1 && Mathf.Sign(verticalSpeed) == -1)
			{
				hasEnded &= true;
			}
			else
			{
				hasEnded &= false;
			}
			return hasEnded;
		}

		private void Stop()
		{
			active = false;
		}

		private void Play()
		{
			active = true;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (deactivateWithDrag)
			{
				Stop();
				if (comeBackAfterDeactivateWithDrag)
				{
					StartCoroutine(WaitToActive());
				}
			}
		}

		private IEnumerator WaitToActive()
		{
			yield return new WaitForSecondsRealtime(waitSeconds);
			Play();
		}
	}
}