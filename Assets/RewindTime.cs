using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RewindTime : MonoBehaviour
{
     List<Vector3> positions;
    public bool isRewinding = false;
    [SerializeField] 
    Rigidbody rb;
    void Start()
    {
        positions = new List<Vector3>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRewinding) Rewind();
        else Record();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Rewind();
        }
    }
    void Rewind()
    {
        if (positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);  //remove right after...
        }
        else StopRewind();
    }
    void Record()
    {
        positions.Insert(0, transform.position);
    }
    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }
    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }
}
