using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    void Update()
    {
        if (target == null) return;

        // Move towards the target smoothly
        //transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        transform.position = target.position;
    }
}
