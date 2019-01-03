using UnityEngine;
using UnityEngine.SceneManagement;

namespace LUT.Snippets
{
	public sealed class LoadScene : MonoBehaviour
	{
		public void Load(int index)
		{
			SceneManager.LoadScene(index);
		}

		public void Load(string name)
		{
			SceneManager.LoadScene(name);
		}
	}
}
