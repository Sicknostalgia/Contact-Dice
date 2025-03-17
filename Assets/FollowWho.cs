using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FollowWho : MonoBehaviour
{
    [SerializeField] Transform diceTrans = null;
    [SerializeField] Vector3 offset;
    CinemachineFreeLook freeLook;
    bool hasAssigned = true;

    private void Start()
    {
        InitializePos();
    }
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
        if (!hasAssigned)
        {
          //  InitializePos();
            hasAssigned = true;
        }
    }


    void InitializePos()
    {
        transform.position = diceTrans.position + offset;
    }
    void DeAssigned()
    {
        //follow player + offset
        gameObject.TryGetComponent<CinemachineFreeLook>(out CinemachineFreeLook cin);
        cin.Follow = null;
        cin.LookAt = null;
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
