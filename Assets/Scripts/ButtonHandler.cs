using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IPointerDownHandler
{
	private Selectable selectable;
	void Start()
	{
		selectable = GetComponent<Selectable>();
	}
	public void OnDeselect(BaseEventData eventData)
	{
		selectable.OnPointerExit(null);
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.selectedObject.GetComponent<Button>() != null)
		{
			GetComponent<Button>().onClick.Invoke();
		}
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		selectable.Select();
	}

	
}
	
