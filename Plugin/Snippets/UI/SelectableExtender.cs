using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace LUT.Snippets.UI
{
	public class SelectableExtender : MonoBehaviour, ISelectHandler, IDeselectHandler
	{
		public UnityEvent onSelect = new UnityEvent();
		public UnityEvent onDeselect = new UnityEvent();

		public void OnDeselect(BaseEventData eventData)
		{
			onDeselect.Invoke();
		}

		public void OnSelect(BaseEventData eventData)
		{
			onSelect.Invoke();
		}

	}
}
