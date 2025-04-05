using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HoverOverBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float scaleModifier = 0.1f;
    private Vector3 defaultScale = new Vector3(1,1,1);
    public Ease easeType;
    public bool isHovered = false;
    private Button button;   //menu buttons
    private void Awake()
    {
      //  SetDefaultScale();
        button = GetComponent<Button>();
    }

/*    public void SetDefaultScale()
    {
        defaultScale = transform.localScale;
    }*/
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = defaultScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.IsInteractable())
        {
            isHovered = true;
            transform.DOScale(defaultScale * scaleModifier, .05f).SetEase(easeType);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        transform.DOScale(defaultScale, .05f).SetEase(easeType);
    }
}
