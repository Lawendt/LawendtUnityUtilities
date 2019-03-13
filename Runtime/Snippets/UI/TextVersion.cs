using UnityEngine;
using UnityEngine.UI;

namespace LUT.Snippets.UI
{
	public sealed class TextVersion : MonoBehaviour
	{
		[SerializeField]
		[AutoFind(typeof(Text))]
		private Text _text;

        private void Reset()
        {
            _text = GetComponentInChildren<Text>();
        }

        private void OnEnable()
		{
			_text.text = "v" + Application.version;
		}
	}
}
