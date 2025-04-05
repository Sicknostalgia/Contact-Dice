using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    float tick;
    float tickRate = .1f;
    bool isPressing;
    public UnityEvent onLongPress;
    public void OnPointerDown(PointerEventData pointerEvent)
    {
        isPressing = true;
    }
    public void OnPointerUp(PointerEventData pointerEvent)
    {
        Reset();
    }
    private void Update()
    {
        if (isPressing)
        {

            tick += Time.unscaledDeltaTime;
            
            if (tick >= tickRate)
            {
                if (onLongPress != null)
                {
                    onLongPress.Invoke();
                    tick = 0;
                }


            }
        }
    }
    void Reset()
    {
        isPressing = false;
        tick = 0;
    }
}
