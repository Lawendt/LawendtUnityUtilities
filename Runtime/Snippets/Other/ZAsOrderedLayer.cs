using UnityEngine;

namespace Law.Utils
{

	[RequireComponent(typeof(Renderer))]
	public class ZAsOrderedLayer : MonoBehaviour
	{
		private Renderer target;

		private void Start()
		{
			target = GetComponent<Renderer>();
		}

		// Update is called once per frame
		private void Update()
		{
			target.sortingOrder = (int)(target.transform.position.z * -100);
		}
	}
}
