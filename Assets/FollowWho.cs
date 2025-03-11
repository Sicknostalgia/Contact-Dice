using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FollowWho : MonoBehaviour
{
    [SerializeField]Transform diceTrans;
    [SerializeField] Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if (!ManageScene.isPlaying)
        {
            //follow player + offset
            transform.position = diceTrans.position + offset;
           gameObject.TryGetComponent<CinemachineFreeLook>(out CinemachineFreeLook cin);
            cin.Follow = null;
            cin.LookAt = null;
        } 
    }
}
