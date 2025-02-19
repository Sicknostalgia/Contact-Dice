using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(TextMeshProUGUI))]
public class NumberCounter : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int CountFPS = 30;
    public float Duration = 1f;
    public string NumberFormat;
    private int _value;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            UpdateText(value);
            _value = value;
        }
    }
    private Coroutine CountingCoroutine;
    private void Awake()
    {
        if (TryGetComponent<TextMeshProUGUI>(out text))
        {
            Debug.Log("Theres text");
        }
        else
        {
            Debug.LogWarning("Nulltext");
        }
    }
    void UpdateText(int newValue)
    {
        if (CountingCoroutine != null)
        {
            StopCoroutine(CountingCoroutine);
        }
        CountingCoroutine = StartCoroutine(CountText(newValue));
    }
    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds wait = new WaitForSeconds(1f / CountFPS);
        int previousValue = _value;
        int stepAmout;

        if (newValue - previousValue < 0)
        {
            stepAmout = Mathf.FloorToInt((newValue - previousValue) / (CountFPS * Duration));
        }
        else
        {
            stepAmout = Mathf.CeilToInt((newValue - previousValue) / (CountFPS * Duration));
        }
        if (previousValue < newValue)
        {
            while (previousValue < newValue)
            {
                previousValue += stepAmout;
                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }
                text.SetText(previousValue.ToString(NumberFormat));
                StartCoroutine(Scaling());
                yield return wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmout;
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }
                text.SetText(previousValue.ToString(NumberFormat));
                StartCoroutine(Scaling());
                yield return wait;
            }
        }
    }
    private IEnumerator Scaling()
    {
        Vector3 originalScale = text.transform.localScale;
        Vector3 targetScale = originalScale * 1.2f;

        float duration = 0.1f;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            text.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;

        }
        elapsedTime = 0f;
/*
        while (elapsedTime < duration)
        {
            text.transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
*/
        text.transform.localScale = originalScale;

    }
}
