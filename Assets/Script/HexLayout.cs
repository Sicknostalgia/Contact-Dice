using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HexLayout : MonoBehaviour
{
    public Transform[] hexTransform;
    public Transform[] squareTransform;
    public Transform foundation;
    public float radius = 5f;
    public Ease ease;
    private void Start()
    {
        //SetDefaultScale();
        FormHexagon();
       // ForSquare();
        RewindTime.onPlace += FormHexagon;
        RewindTime.notOnPlace += Scatter;
    }
    void FormHexagon()
    {
        if (hexTransform.Length != 6)
        {
            Debug.LogError("More/less than 6");
            return;
        }
        for (int i = 0; i < hexTransform.Length; i++)
        {
            float angle = i * 60 * Mathf.Deg2Rad;  // Convert deg to rad since we will use mathf.Cos/Sin
            Vector3 position = new Vector3(
                foundation.position.x + Mathf.Cos(angle) * radius,
                foundation.position.y,
                foundation.position.z + Mathf.Sin(angle) * radius);

            hexTransform[i].position = position;
            hexTransform[i].DOScale(1, .5f).SetEase(ease);
        }

    }
 /*   void ForSquare()
    {
        if (squareTransform.Length < 4)
        {
            Debug.Log("Need at least 4 objects to form a square.");
            return;
        }

        float halfSize = radius;

        Vector3[] offsets = new Vector3[]
        {
        new Vector3(-halfSize, 0, -halfSize), // Bottom Left
        new Vector3(-halfSize, 0, halfSize),  // Top Left
        new Vector3(halfSize, 0, halfSize),   // Top Right
        new Vector3(halfSize, 0, -halfSize)   // Bottom Right
        };

        for (int i = 0; i < 4; i++)
        {
            Vector3 pos = foundation.position + offsets[i];
            squareTransform[i].position = pos;
            squareTransform[i].DOScale(1, .5f).SetEase(ease);
        }
    }*/

    private void Update() //looking to camera anytime
    {
        for (int i = 0; i < hexTransform.Length; i++)
        {
            hexTransform[i].LookAt(Camera.main.transform);
            Vector3 dirToCamera = Camera.main.transform.position + hexTransform[i].position;
            dirToCamera.x = 0f;
            dirToCamera.y = -360f;

            hexTransform[i].rotation = Quaternion.LookRotation(dirToCamera);
        }
    }

    private void Scatter()  // place above as UI    and then there must be a regroup function
    {
        for (int i = 0; i < hexTransform.Length; i++)
        {
            hexTransform[i].DOScale(0, .5f);
        }
    }

    private void OnDisable()
    {
        RewindTime.onPlace -= FormHexagon;
        RewindTime.notOnPlace -= Scatter;
    }
}
