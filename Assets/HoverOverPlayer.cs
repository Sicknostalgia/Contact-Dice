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
    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke();
        isPointerInside = true;
        selectionOutlineMat.SetFloat("_scale", value); // Modify shader variable
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke();
        isPointerInside = false;
        selectionOutlineMat.SetFloat("_scale", 0); // Modify shader variable
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onPointerClick?.Invoke();
    }
}
