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
        transform.position = target.position;
    }
}
