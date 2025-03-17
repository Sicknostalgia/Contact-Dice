using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOverPlayer : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Material selectionOutlineMat;
    public void OnPointerEnter(PointerEventData eventData)
    {
        float value = Mathf.PingPong(Time.time, 1.1f);
        selectionOutlineMat.SetFloat("_scale", value); // Modify shader variable
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        float value = Mathf.PingPong(Time.time, 0f);
        selectionOutlineMat.SetFloat("_scale", value); // Modify shader variable
    }

    public void OnPointerClick(PointerEventData eventData)
    {

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
