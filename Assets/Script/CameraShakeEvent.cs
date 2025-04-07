using System;
using UnityEngine;

public class CameraShakeEvent : MonoBehaviour
{
    public static event Action<float, float> OnShake;

    public static void TriggerShake(float intensity, float duration)
    {
        OnShake?.Invoke(intensity, duration);
    }
}
