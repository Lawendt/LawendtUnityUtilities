using UnityEngine;

namespace Law
{

	public class FollowMouse : MonoBehaviour
	{
		[SerializeField]
		protected new Camera camera;

		[SerializeField]
		protected bool invert = false;

		protected void Reset()
		{
			camera = Camera.main;
		}
		protected virtual void OnEnable()
		{
			if (!camera)
			{
				camera = Camera.main;
			}

		}

		protected void Update()
		{
			Vector3 mousePosition = Input.mousePosition;
			if (invert)
			{
				mousePosition.x = Screen.width - mousePosition.x;
				mousePosition.y = Screen.height - mousePosition.y;
			}
			mousePosition.z = camera.transform.position.z * -1;

			transform.position = camera.ScreenToWorldPoint(mousePosition);
		}
	}
}