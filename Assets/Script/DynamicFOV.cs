using UnityEngine;
using Cinemachine;
public class DynamicFOV : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    void Start()
    {
        float targetAspect = 9f / 16f;
        float currentAspect = (float)Screen.width / Screen.height;

        if (vCam != null)
        {
            if (currentAspect < targetAspect)
            {
                vCam.m_Lens.FieldOfView = 60f;
            }
            else
            {
                vCam.m_Lens.FieldOfView = 95f;
            }
        }
    }
}
