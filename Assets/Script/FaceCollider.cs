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
    public UnityEvent<string> OnFaceDetected;

    private NormalVector normVec;

    public Sprite spriteRight;
    public Sprite spriteLeft;
    public Sprite spriteTop;
    public Sprite spriteBottom;
    public Sprite spriteFront;
    public Sprite spriteBack;
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

    private Dictionary<NormalVector, Sprite> faceTex;
    private void Start()
    {
        faceTex = new Dictionary<NormalVector, Sprite>()
        {
            { NormalVector.right,spriteRight },
            {NormalVector.left,spriteLeft },
            {NormalVector.top,spriteTop },
            {NormalVector.bottom,spriteBottom },
            {NormalVector.front,spriteFront },
            {NormalVector.back, spriteBack }
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
        if (TryGetComponent<VisualEffect>(out VisualEffect vfx))
        {
            Sprite finalSprite = GetSprite(face);
            vfx.SetTexture("diceNumTex", finalSprite.texture);
        }

        CameraShakeEvent.TriggerShake(1, .25f);
        ObjctPlTrnsfrm.SpawnObject(vfx.gameObject, centerOfFace, Quaternion.identity);
    }

    private Sprite GetSprite(NormalVector face)
    {
        return faceTex.TryGetValue(face, out Sprite sprite) ? sprite : null;
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

