using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleHandleIt : MonoBehaviour
{
    private Vector3 hitPoint;
    private Vector3 hitNormal;
    private bool hitDetected = false;
    public LayerMask groundLayer;
    public GameObject vfx;
    /*   void Update()
       {
           DetectFace();
       }*/

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0]; // Get the first contact point
        hitPoint = contact.point;
        hitNormal = contact.normal;
        hitDetected = true;


        string nearestFace = GetNearestFace(hitPoint, transform);
        Vector3 faceDirection = GetFaceDirection(nearestFace); // Convert face name to direction
        Vector3 centerOfFace = transform.position + (faceDirection * transform.lossyScale.magnitude / 2f); 
        string face = GetColliderFace(hitNormal, transform);

        Debug.Log(GetNearestFace(hitPoint,transform));
        CameraShakeEvent.TriggerShake(1, .25f);
        ObjctPlTrnsfrm.SpawnObject(vfx, centerOfFace,Quaternion.identity);

       // Debug.Log("Collided with: " + collision.gameObject.name + " on face: " + face);
        //get the surface normal of the collider pole
        //if(pole face touch the terrain then destroy the pole

    }
    /* void OnCollisionEnter(Collision collision)
     {
         // Check if collided object is in the ground layer
         if (((1 << collision.gameObject.layer) & groundLayer) != 0)
         {
             // Get the contact point normal
             Vector3 normal = collision.contacts[0].normal;

             // Determine the face based on normal direction
             if (normal == Vector3.up) Debug.Log("Bottom Face (Down) hit the ground.");
             else if (normal == Vector3.down) Debug.Log("Top Face (Up) hit the ground.");
             else if (normal == Vector3.left) Debug.Log("Right Face hit the ground.");
             else if (normal == Vector3.right) Debug.Log("Left Face hit the ground.");
             else if (normal == Vector3.forward) Debug.Log("Back Face hit the ground.");
             else if (normal == Vector3.back) Debug.Log("Front Face hit the ground.");
         }*/
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
            default: return Vector3.zero; // Default case if something goes wrong
        }
    }
    string GetNearestFace(Vector3 hitPoint, Transform cubeTransform)
    {
        // Convert hit point to local space
        Vector3 localPoint = cubeTransform.InverseTransformPoint(hitPoint);

        // Get the absolute distances to the center
        float absX = Mathf.Abs(localPoint.x);
        float absY = Mathf.Abs(localPoint.y);
        float absZ = Mathf.Abs(localPoint.z);

        // Compare which axis is dominant
        if (absX >= absY && absX >= absZ) // X is dominant
        {
            return (localPoint.x > 0) ? "4 Right (+X)" : "3 Left (-X)";
        }
        else if (absY > absX && absY > absZ) // Y is dominant
        {
            return (localPoint.y > 0) ? "6 Top (+Y)" : "1 Bottom (-Y)";
        }
        else // Z is dominant
        {
            return (localPoint.z > 0) ? "5 Front (+Z)" : "2 Back (-Z)";
        }
    }
    string GetColliderFace(Vector3 normal,Transform cubeTrans)
    {
        Vector3 localNormal = cubeTrans.InverseTransformDirection(normal);//Convert to Local Space. detect faces relative to the cube's orientation
        //pag di natin kinonvert we still received the axis of the world space, youll always get the Top(+Y) result.
        if (Vector3.Dot(localNormal, Vector3.right) > 0.7f) return "Right (+X)";
        if (Vector3.Dot(localNormal, Vector3.left) > 0.7f) return "Left (-X)";
        if (Vector3.Dot(localNormal, Vector3.up) > 0.7f) return "Top (+Y)";
        if (Vector3.Dot(localNormal, Vector3.down) > 0.7f) return "Bottom (-Y)";
        if (Vector3.Dot(localNormal, Vector3.forward) > 0.7f) return "Front (+Z)";
        if (Vector3.Dot(localNormal, Vector3.back) > 0.7f) return "Back (-Z)";

        return "Unknown";
    }

    void OnDrawGizmos()
    {
        /* if (hitDetected)
         {
             // Draw the hit point
             Gizmos.color = Color.red;
             Gizmos.DrawSphere(hitPoint, 0.1f);

             // Draw the normal direction
             Gizmos.color = Color.blue;
             Gizmos.DrawRay(hitPoint, hitNormal * 0.5f);
         }*//*
        if (hitDetected)
        {
            // Draw the contact point
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hitPoint, 0.05f);

            // Draw the normal vector
            Gizmos.color = Color.green;
            Gizmos.DrawLine(hitPoint, hitPoint + hitNormal * 0.5f);

            // Reset hitDetected for visualization refresh
            hitDetected = false;
        }*/
        // Set gizmo colors and draw lines for each face direction
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
    
