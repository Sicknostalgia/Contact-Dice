using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class FaceCollider : MonoBehaviour
{
    private Vector3 hitPoint;
    private Vector3 hitNormal;
    private bool hitDetected = false;
    public LayerMask groundLayer;
    public GameObject vfx;
   // public UnityEvent<string> OnFaceDetected;

    private NormalVector normVec;

    public Texture2D Texture2DRight;
    public Texture2D Texture2DLeft;
    public Texture2D Texture2DTop;
    public Texture2D Texture2DBottom;
    public Texture2D Texture2DFront;
    public Texture2D Texture2DBack;
    public enum NormalVector
    {
        right,
        left,
        top,
        bottom,
        front,
        back,
        Unknown
    }

    private Dictionary<NormalVector, Texture2D> faceTex;
    private void Start()
    {
        faceTex = new Dictionary<NormalVector, Texture2D>()
        {
            {NormalVector.right,Texture2DRight },
            {NormalVector.left,Texture2DLeft },
            {NormalVector.top,Texture2DTop },
            {NormalVector.bottom,Texture2DBottom },
            {NormalVector.front,Texture2DFront },
            {NormalVector.back, Texture2DBack }
        };
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        hitPoint = contact.point;
        hitNormal = contact.normal;
        hitDetected = true;

        NormalVector face = GetColliderFace(hitNormal, transform);
        Vector3 faceDirection = GetFaceDirection(normVec);
        Vector3 centerOfFace = transform.position + (faceDirection * transform.lossyScale.magnitude / 2f);

        Debug.Log(face);
        if (vfx.gameObject.TryGetComponent<VisualEffect>(out VisualEffect vfxCom))
        {
            Debug.Log("ey");
            Texture2D finalTexture2D = GetTexture2D(face);
        }
        else
        {
            Debug.Log("null vfx");
        }

        CameraShakeEvent.TriggerShake(1, .25f);
        ObjctPlTrnsfrm.SpawnObject(vfx.gameObject, centerOfFace, Quaternion.identity);
    }

    private Texture2D GetTexture2D(NormalVector face)
    {
        return faceTex.TryGetValue(face, out Texture2D Texture2D) ? Texture2D : null;
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

            NormalVector face = GetColliderFace(hit.normal, transform);
            Debug.Log("Hit face: " + face);
        }
    }
    Vector3 GetFaceDirection(NormalVector norm)
    {
        switch (norm)
        {
            case NormalVector.right: return transform.right;
            case NormalVector.left: return -transform.right;
            case NormalVector.top: return transform.up;
            case NormalVector.bottom: return -transform.up;
            case NormalVector.front: return transform.forward;
            case NormalVector.back: return -transform.forward;
            default: return Vector3.zero;
        }
    }


    NormalVector GetColliderFace(Vector3 normal, Transform cubeTrans)
    {
        Vector3 localNormal = cubeTrans.InverseTransformDirection(normal);
        if (Vector3.Dot(localNormal, Vector3.right) > 0.7f) return NormalVector.right;
        if (Vector3.Dot(localNormal, Vector3.left) > 0.7f) return NormalVector.left;
        if (Vector3.Dot(localNormal, Vector3.up) > 0.7f) return NormalVector.top;
        if (Vector3.Dot(localNormal, Vector3.down) > 0.7f) return NormalVector.bottom;
        if (Vector3.Dot(localNormal, Vector3.forward) > 0.7f) return NormalVector.front;
        if (Vector3.Dot(localNormal, Vector3.back) > 0.7f) return NormalVector.back;

        return NormalVector.Unknown;
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

