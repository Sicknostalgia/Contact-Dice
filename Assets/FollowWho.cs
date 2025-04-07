using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FollowWho : MonoBehaviour
{
    [SerializeField] Transform diceTrans;
    [SerializeField] Vector3 offset;
    CinemachineVirtualCamera topDownVirt;
    public Transform origTrans;
    private void Start()
    {
        RewindTime.onPlace += DeAssigned;
        InitializePos();
        origTrans.position = transform.position + offset;
        origTrans.rotation = transform.rotation;
        TryGetComponent<CinemachineVirtualCamera>(out topDownVirt);
    }
    
    public void InitializePos()
    {
        transform.position = diceTrans.position + offset;
    }
    public void DeAssigned()
    {
        topDownVirt.Follow = null;
        topDownVirt.LookAt = null;

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
