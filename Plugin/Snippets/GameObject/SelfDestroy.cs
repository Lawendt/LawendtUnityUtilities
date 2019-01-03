using UnityEngine;

namespace LUT
{

	public sealed class SelfDestroy : MonoBehaviour
	{
		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}
