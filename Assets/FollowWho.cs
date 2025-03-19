using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FollowWho : MonoBehaviour
{
    [SerializeField] Transform diceTrans = null;
    [SerializeField] Vector3 offset;
    CinemachineVirtualCamera topDownVirt;
    public bool hasAssigned = true;
    private void Start()
    {
        RewindTime.onPlace += DeAssigned;
        InitializePos();
        TryGetComponent<CinemachineVirtualCamera>(out topDownVirt);
    }
    private void Update()
    {

        if (hasAssigned)
        {
            DeAssigned();
            hasAssigned = false;
        }

    }
    public void InitializePos()
    {
        transform.position = diceTrans.position + offset;
    }
    public void DeAssigned()
    {
        //follow player + offset  
        topDownVirt.Follow = null;
        topDownVirt.LookAt = null;
        InitializePos();
    }
    public void Assigned()
    {
        topDownVirt.Follow = diceTrans;
        topDownVirt.LookAt = diceTrans;
    }

    private void OnApplicationQuit()
    {
        RewindTime.onPlace -= DeAssigned;
    }
}
