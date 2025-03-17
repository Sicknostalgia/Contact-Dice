using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FollowWho : MonoBehaviour
{
    [SerializeField] Transform diceTrans = null;
    [SerializeField] Vector3 offset;
    CinemachineFreeLook freeLook;
    private void Update()
    {
        if (!ManageScene.isPlaying)
        {
            DeAssigned();
        }
        /*if (!ManageScene.isPlaying)
        {
            DeAssigned();

        }*/
    }

    void DeAssigned()
    {
        //follow player + offset
        gameObject.TryGetComponent<CinemachineFreeLook>(out CinemachineFreeLook cin);
        cin.Follow = null;
        cin.LookAt = null;
       transform.position = diceTrans.position + offset;
        return;  //Play once to prevent constant position assignment

    }
    void Assigned()
    {
        gameObject.TryGetComponent<CinemachineFreeLook>(out CinemachineFreeLook yey);
        yey.Follow = diceTrans;
        yey.LookAt = diceTrans;
    }
}
