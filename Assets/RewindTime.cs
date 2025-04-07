using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RewindTime : MonoBehaviour
{
    List<PointInTime> pointsInTime;
    List<PointInTimeAudio> audioinTime;
    public bool isRewinding = false;
    [SerializeField]
    Rigidbody rb;
    float gradualRwind;
    public MenuMvment menuMvmnt;
    [SerializeField] FaceCollider faceCol;
    [SerializeField] CinemachineFreeLook freeLook;
    public static event Action onPlace;
    public static event Action notOnPlace;
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude != 0)
        {
            if (!isRewinding)
            {
            Record();
            }
        }
    }

    public static void TrigOnPlace()
    {
        onPlace?.Invoke();

    }
    public static void TrigNotOnPlace()
    {
        notOnPlace?.Invoke();
    }
    IEnumerator Rewind()
    {
        while (pointsInTime.Count > 0)
        {
            PointInTime pointIT = pointsInTime[0];
            transform.position = pointIT.position;
            transform.rotation = pointIT.rotation;

            pointsInTime.RemoveAt(0);
            yield return new WaitForSeconds(.001f);
        }
        StopRewind();
    }

    void Record()
    {
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StartRewind()
    {
        isRewinding = true;
        StartCoroutine(Rewind());
        rb.isKinematic = true;
    }
    public void StopRewind()
    {
        FaceCollider.hasResult = false;
        HoverOverPlayer.isPlaying = false;
        isRewinding = false;
        rb.isKinematic = false;
        menuMvmnt.RunSequence();
        freeLook.gameObject.SetActive(false);
        TrigOnPlace();
    }
}
