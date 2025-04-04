using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using DG.Tweening;
public class HoverOverPlayer : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Material selectionOutlineMat;
    bool isPointerInside = false;
    [SerializeField] float value;
    public static event Action onPointerEnter;
    public static event Action onPointerExit;
    public UnityEvent onPointerClick;

    public static bool isPlaying = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isPlaying) return;
        onPointerEnter?.Invoke();
        isPointerInside = true;
        selectionOutlineMat.SetFloat("_scale", value); // Modify shader variable
        transform.DOScale(1,.01f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPlaying) return;
        onPointerExit?.Invoke();
        isPointerInside = false;
        selectionOutlineMat.SetFloat("_scale", 0); // Modify shader variable
        transform.DOScale(.7f, .01f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPlaying) return;
        onPointerClick?.Invoke();
        selectionOutlineMat.SetFloat("_scale", 0); // Modify shader variable
        transform.DOScale(.7f, .01f);
        RewindTime.TrigNotOnPlace();
        isPlaying = true;
    }
}
