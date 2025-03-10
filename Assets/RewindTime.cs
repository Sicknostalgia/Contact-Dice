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
            transform.position = new Vector3(Random.Range(192.58f, 212.4f), 248, Random.Range(90.46f, 101.5f));
            for (int i = 0; i < faceCol.ButGroup.Length; i++)
            {
                faceCol.ButGroup[i].transform.localScale = faceCol.originalScale;
            }
        }
    }
}
