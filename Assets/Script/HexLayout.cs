using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexLayout : MonoBehaviour
{
    public Transform[] vertexPTransform;
    public Transform foundation;
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
                foundation.position.x + Mathf.Cos(angle) * radius, 
                foundation.position.y, 
                foundation.position.z + Mathf.Sin(angle) * radius);
            vertexPTransform[i].position = position;
        }

    }
    private void Update()
    {
        for (int i = 0; i < vertexPTransform.Length; i++)
        {
            vertexPTransform[i].LookAt(Camera.main.transform);
            Vector3 dirToCamera = Camera.main.transform.position + vertexPTransform[i].position;
            dirToCamera.x = 0f;
            dirToCamera.y = -360f;

            vertexPTransform[i].rotation = Quaternion.LookRotation(dirToCamera);
        }
    }
}
