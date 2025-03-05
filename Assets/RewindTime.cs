using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RewindTime : MonoBehaviour
{
     List<PointInTime> pointsInTime;
    public bool isRewinding = false;
    [SerializeField] 
    Rigidbody rb;
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRewinding) StartCoroutine(Rewind());
        else Record();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Rewind();
        }
    }
    IEnumerator Rewind()
    {
        while (pointsInTime.Count > 0)
        {
            PointInTime pointIT = pointsInTime[0];
            transform.position = pointIT.position;
            transform.rotation = pointIT.rotation;
            pointsInTime.RemoveAt(0);  //remove right after...
            yield return null;
        }
        StopRewind();
    }
    void Record()
    {
        pointsInTime.Insert(0, new PointInTime(transform.position,transform.rotation));
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
