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

    // public CineShake cineShake;

    /// <summary>
    /// Observe Pattern for more scalable event sytem
    /// </summary>
    public static event Action onPlace;
    public static event Action notOnPlace;
    void Start()
    {

        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();


    }
    void FixedUpdate()
    {
        if (!isRewinding)
        {
            Record();
        }
    }

    public static void TrigOnPlace() //placing event to this make it scalable
    {
        onPlace?.Invoke();

    }
       public static void TrigNotOnPlace() //placing event to this make it scalable
        {
            notOnPlace?.Invoke();
        }

    private void Update()
    {
        /*   if (Input.GetKey(KeyCode.Space))
           {
               Rewind();
           }*/
    }

    IEnumerator Rewind()
    {
        while (pointsInTime.Count > 0)
        {
            PointInTime pointIT = pointsInTime[0];
            transform.position = pointIT.position;
            transform.rotation = pointIT.rotation;

            pointsInTime.RemoveAt(0);  //remove right after...
            yield return new WaitForSeconds(.001f);
        }
        StopRewind();
    }

    void Record()
    {
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
        /*        audioinTime.Insert(0, new PointInTimeAudio(cineShake.collisionSound));*/
    }

    public void StartRewind()
    {
        isRewinding = true;
        StartCoroutine(Rewind());
        rb.isKinematic = true;
        ResetScale();
    }
    void ResetScale()
    {
        //seperate this as well
        for (int i = 0; i < faceCol.ButGroup.Length; i++)  //reset the scale of the button ui
        {
            faceCol.ButGroup[i].transform.localScale = faceCol.originalScale;
        }
    }
    public void StopRewind()
    {
        FaceCollider.hasResult = false;
        HoverOverPlayer.isPlaying = false;
        isRewinding = false;
        rb.isKinematic = false;
        /*        if (transform.position.y >= originalPos.y - 2)  //above OnPlace
                {*/
        menuMvmnt.RunSequence();  // we can't put this on isRewinding since it needs to trigger once
        freeLook.gameObject.SetActive(false);
        TrigOnPlace();
        /*}*/
        /*     else
             {
                 TrigNotOnPlace();
             }*/
    }
}
