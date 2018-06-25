using UnityEngine.SceneManagement;
using UnityEngine;

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
