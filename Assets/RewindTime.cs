using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTime : MonoBehaviour
{
    List<Vector3> positions;
    bool isRewinding = false;
    void Start()
    {
        positions = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartRewind()
    {
        isRewinding = true;
    }
    public void StopRewind()
    {
        isRewinding = false;
    }
}
