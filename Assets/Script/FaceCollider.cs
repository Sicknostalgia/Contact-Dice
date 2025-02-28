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
    public UnityEvent<int> OnDiceValue;

    public NumberCounter numberCounter;
    public NormalVector normVec;

    public Texture2D Texture2DRight;
    public Texture2D Texture2DLeft;
    public Texture2D Texture2DTop;
    public Texture2D Texture2DBottom;
    public Texture2D Texture2DFront;
    public Texture2D Texture2DBack;
    [SerializeField] DisplayTextCtrler disCtrlr;
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
    /*public int NumEnumChange(NormalVector normVec)
    {
        switch (normVec)
        {
            case NormalVector.right:
                return 3;
            case NormalVector.left:
                return 4;
            case NormalVector.front:
                return 2;
            case NormalVector.back:
                return 5;
            case NormalVector.top:
                return 6;
            case NormalVector.bottom:
                return 1;
            default: return 0;
                
        }
    }*/
    public int NumEnumChange(NormalVector normVec)
    {
        switch (normVec)
        {
            case NormalVector.right:
                return 3;
            case NormalVector.left:
                return 4;
            case NormalVector.front:
                return 2;
            case NormalVector.back:
                return 5;
            case NormalVector.top:
                return 6;
            case NormalVector.bottom:
                return 1;
            default: return 0;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        hitPoint = contact.point;
        hitNormal = contact.normal;
        hitDetected = true;

        NormalVector face = GetColliderFace(hitNormal, transform); //this calculate the final normal vector result, failed to reference this wont change your value on the following line
        Vector3 faceDirection = GetFaceDirection(face);
        numberCounter.Value = NumEnumChange(face);  //update value of dice number on UI
        
        Vector3 centerOfFace = transform.position + (faceDirection * transform.lossyScale.magnitude / 2f);
        Debug.Log(face);
        if (vfx.gameObject.TryGetComponent<VisualEffect>(out VisualEffect vfxCom))
        {
            Debug.Log("ey");
            Texture2D finalTexture2D = GetTexture2D(face);
            vfxCom.SetTexture("MainTex", finalTexture2D);
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
        return faceTex.TryGetValue(face, out Texture2D texture2D) ? texture2D : null;
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
    private bool hasLogged = false;

    void Update()
    {
        if (gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            if (rb.velocity.magnitude == 0 && !hasLogged)
            {
                Debug.Log(GetFaceDirection(normVec));
                numberCounter.Value = NumEnumChange(GetColliderFace(hitNormal,transform));  //equivalent to face
                disCtrlr.UpdatePara();
                //here dialogue final value
                //disCtrlr.ParagraphUpdate(face);
                hasLogged = true;
            }
            if (rb.velocity.magnitude > 0)
            {
                hasLogged = false;
            }
        }
        /*        DrawArrow(transform.position, transform.right, Color.red);   // Right (+X)
                DrawArrow(transform.position, -transform.right, Color.red);  // Left (-X)
                DrawArrow(transform.position, transform.up, Color.green);    // Top (+Y)
                DrawArrow(transform.position, -transform.up, Color.green);   // Bottom (-Y)
                DrawArrow(transform.position, transform.forward, Color.blue);// Front (+Z)
                DrawArrow(transform.position, -transform.forward, Color.blue);// Back (-Z)*/
    }

    private void DrawArrow(Vector3 start, Vector3 direction, Color color)
    {
        Vector3 end = start + direction * 1.5f; // Adjust arrow length

        // Draw main line
        Debug.DrawLine(start, end, color);

        // Draw arrowhead
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 150, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -150, 0) * Vector3.forward;
        Debug.DrawLine(end, end + right * 0.3f, color);
        Debug.DrawLine(end, end + left * 0.3f, color);
    }
}

