using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexLayout : MonoBehaviour
{
    public Transform[] vertexPTransform;
    public float radius = 5f;

    private void Start()
    {
        InitiateHexagon();
    }

    void InitiateHexagon()
    {
        if(vertexPTransform.Length != 6)
        {
            Debug.LogError("More/less than 6");
            return;
        }    
        for (int i = 0; i < vertexPTransform.Length; i++)
        {
            float angle = i * 60 * Mathf.Deg2Rad;  // Convert deg to rad since we will use mathf.Cos/Sin
            Vector3 position = new Vector3(
                Mathf.Cos(angle) * radius, 
                0f, 
                Mathf.Sin(angle) * radius);
            vertexPTransform[i].position = position;
        }

    }
}
