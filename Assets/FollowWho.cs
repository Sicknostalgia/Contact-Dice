using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FollowWho : MonoBehaviour
{
    [SerializeField] Transform diceTrans = null;
    [SerializeField] Vector3 offset;
    CinemachineFreeLook freeLook;
    bool hasAssigned = false;
    private void Update()
    {
        if (!ManageScene.isPlaying && hasAssigned)
        {
            Assigned();
        }
        if (!ManageScene.isPlaying && !hasAssigned)
        {
            DeAssigned();

        }
    }

    void DeAssigned()
    {
        //follow player + offset
        gameObject.TryGetComponent<CinemachineFreeLook>(out CinemachineFreeLook cin);
        cin.Follow = null;
        cin.LookAt = null;
       transform.position = diceTrans.position + offset;
        hasAssigned = false;

    }
    void Assigned()
    {
        gameObject.TryGetComponent<CinemachineFreeLook>(out CinemachineFreeLook yey);
        yey.Follow = diceTrans;
        yey.LookAt = diceTrans;
        hasAssigned = true;
    }
}
