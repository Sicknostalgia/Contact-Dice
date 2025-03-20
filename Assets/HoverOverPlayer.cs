using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HoverOverPlayer : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Material selectionOutlineMat;
    bool isPointerInside = false;
    [SerializeField] float value;
    public UnityEvent onPointerEnter;
    public UnityEvent onPointerExit;
    public UnityEvent onPointerClick;

    public static bool isPlaying = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isPlaying) return;
        onPointerEnter?.Invoke();
        isPointerInside = true;
        selectionOutlineMat.SetFloat("_scale", value); // Modify shader variable
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPlaying) return;
        onPointerExit?.Invoke();
        isPointerInside = false;
        selectionOutlineMat.SetFloat("_scale", 0); // Modify shader variable
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPlaying) return;
        onPointerClick?.Invoke();
    }
}
