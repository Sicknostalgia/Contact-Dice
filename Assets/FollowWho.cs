using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FollowWho : MonoBehaviour
{
    [SerializeField] Transform diceTrans;
    [SerializeField] Vector3 offset;
    CinemachineFreeLook freeLook;
    private void Update()
    {
        if (ManageScene.isPlaying)
        {
            Assigned();
        }
        if (!ManageScene.isPlaying)
        {
            DeAssigned();

        }
    }

    void DeAssigned()
    {
        //follow player + offset
        transform.position = diceTrans.position + offset;
        gameObject.TryGetComponent<CinemachineFreeLook>(out CinemachineFreeLook cin);
        cin.Follow = null;
        cin.LookAt = null;

    }
    void Assigned()
    {
        gameObject.TryGetComponent<CinemachineFreeLook>(out CinemachineFreeLook yey);
        yey.Follow = diceTrans;
        yey.Follow = diceTrans;
    }
}
