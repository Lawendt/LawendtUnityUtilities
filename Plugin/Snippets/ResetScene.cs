using UnityEngine;
using UnityEngine.SceneManagement;

namespace LUT.Snippets
{
	public sealed class ResetScene : MonoBehaviour
	{
		public void ResetCurrentScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
