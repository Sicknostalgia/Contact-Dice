using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class RewindTime : MonoBehaviour
{
    List<PointInTime> pointsInTime;
    List<PointInTimeAudio> audioinTime;
    public bool isRewinding = false;
    [SerializeField]
    Rigidbody rb;
    float gradualRwind;
    Vector3 originalPos;
    [SerializeField] FaceCollider faceCol;
    // public CineShake cineShake;
   public delegate void playerPlace();
    public static event playerPlace onPlace;
    public static event playerPlace notOnPlace;
    void Start()
    {
        originalPos.y = transform.position.y;
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
    }
    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
        if (transform.position.y >= originalPos.y-2)
        {     
            onPlace?.Invoke(); // null check to avoid error

            //seperate this as well
            for (int i = 0; i < faceCol.ButGroup.Length; i++)  //reset the scale of the button ui
            {
                faceCol.ButGroup[i].transform.localScale = faceCol.originalScale;

            }
        }
        else
        {
            notOnPlace?.Invoke();
        }
    }
}
