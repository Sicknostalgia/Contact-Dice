using UnityEngine;
using DG.Tweening;
public class UIEnable : MonoBehaviour
{
    public float punchAmount = 1.1f;
    public Ease ease;
    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            child.DOPunchScale(Vector3.one * punchAmount, 0.5f, 10, 1).SetEase(ease)
                .SetUpdate(true);// run even timescale = 0f;
        }
    }
}
