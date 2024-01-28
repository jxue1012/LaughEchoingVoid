using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartSceneBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameCenter.Instance.uIManager.ButtonSign.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameCenter.Instance.uIManager.ButtonSign.SetActive(false);
    }
}
