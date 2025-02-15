using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCollider : MonoBehaviour
{
    private Vector3 hitPoint;
    private Vector3 hitNormal;
    private bool hitDetected = false;
    public LayerMask groundLayer;
    public GameObject vfx;

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        hitPoint = contact.point;
        hitNormal = contact.normal;
        hitDetected = true;


        string face = GetColliderFace(hitNormal, transform);
        Vector3 faceDirection = GetFaceDirection(face);
        Vector3 centerOfFace = transform.position + (faceDirection * transform.lossyScale.magnitude / 2f);

        Debug.Log(face);
        CameraShakeEvent.TriggerShake(1, .25f);
        ObjctPlTrnsfrm.SpawnObject(vfx, centerOfFace,Quaternion.identity);
    }
    void DetectFace()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hitPoint = hit.point;
            hitNormal = hit.normal;
            hitDetected = true;

            string face = GetColliderFace(hit.normal, transform);
            Debug.Log("Hit face: " + face);
        }
    }
    Vector3 GetFaceDirection(string face)
    {
        switch (face)
        {
            case "Right (+X)": return transform.right;
            case "Left (-X)": return -transform.right;
            case "Top (+Y)": return transform.up;
            case "Bottom (-Y)": return -transform.up;
            case "Front (+Z)": return transform.forward;
            case "Back (-Z)": return -transform.forward;
            default: return Vector3.zero;
        }
    }

    string GetColliderFace(Vector3 normal,Transform cubeTrans)
    {
        Vector3 localNormal = cubeTrans.InverseTransformDirection(normal);
        if (Vector3.Dot(localNormal, Vector3.right) > 0.7f) return "Right (+X)4";
        if (Vector3.Dot(localNormal, Vector3.left) > 0.7f) return "Left (-X)3";
        if (Vector3.Dot(localNormal, Vector3.up) > 0.7f) return "Top (+Y)6";
        if (Vector3.Dot(localNormal, Vector3.down) > 0.7f) return "Bottom (-Y)1";
        if (Vector3.Dot(localNormal, Vector3.forward) > 0.7f) return "Front (+Z)5";
        if (Vector3.Dot(localNormal, Vector3.back) > 0.7f) return "Back (-Z)2";

        return "Unknown";
    }

    void OnDrawGizmos()
    {
        DrawGizmoArrow(transform.position, transform.right, Color.red, "Right (+X)");
        DrawGizmoArrow(transform.position, -transform.right, Color.red, "Left (-X)");

        DrawGizmoArrow(transform.position, transform.up, Color.green, "Top (+Y)");
        DrawGizmoArrow(transform.position, -transform.up, Color.green, "Bottom (-Y)");

        DrawGizmoArrow(transform.position, transform.forward, Color.blue, "Front (+Z)");
        DrawGizmoArrow(transform.position, -transform.forward, Color.blue, "Back (-Z)");
    }

    private void DrawGizmoArrow(Vector3 start, Vector3 direction, Color color, string label)
    {
        Gizmos.color = color;
        Vector3 end = start + direction * 1.5f; // Scale the arrow length

        // Draw the main line
        Gizmos.DrawLine(start, end);

        // Draw arrowhead
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 150, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -150, 0) * Vector3.forward;
        Gizmos.DrawLine(end, end + right * 0.3f);
        Gizmos.DrawLine(end, end + left * 0.3f);

        // Draw label
        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;
        UnityEditor.Handles.Label(end, label, style);
    }
}
    
