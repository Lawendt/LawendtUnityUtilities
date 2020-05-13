using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Law.Utils
{
	public sealed class WaitAndEvent : MonoBehaviour
	{
		[SerializeField]
		private float wait = 1f;
		public UnityEvent DO = new UnityEvent();

		private IEnumerator Start()
		{
			yield return new WaitForSeconds(wait);
			DO.Invoke();
		}
	}
}
