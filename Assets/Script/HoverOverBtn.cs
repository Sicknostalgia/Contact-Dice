using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HoverOverBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float scaleModifier = 0.1f;
    private Vector3 defaultScale;

    public bool isHovered = false;
    private Button button;   //menu buttons
    private void Awake()
    {
        SetDefaultScale();
        button = GetComponent<Button>();
    }

    public void SetDefaultScale()
    {
        defaultScale = transform.localScale;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = defaultScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.IsInteractable())
        {
            isHovered = true;
            transform.DOScale(1, .5f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        transform.DOScale(defaultScale, .5f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
