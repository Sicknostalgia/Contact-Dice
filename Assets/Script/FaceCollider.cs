using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.VFX;
using DG.Tweening;
using Cinemachine;

public class FaceCollider : MonoBehaviour
{
    [Header("Face Normal Calculation")]
    private Vector3 hitPoint;
    private Vector3 hitNormal;
    private bool hitDetected = false;
    public LayerMask groundLayer;
    [Header("Cameras")]
    public CinemachineFreeLook thirdPerson;
    public CinemachineVirtualCamera topdownCam;
    [Header("OtherReferences")]
    public RewindTime rewindTime;
    public NormalVector normVec;
    [SerializeField] DisplayTextCtrler disCtrlr;
    [SerializeField] GameObject decalsObj;
    public GameObject vfx;
    public GameObject panel;
    public FollowWho followWHo;
    private Rigidbody rb;
    [Header("Textures and Buttons")]
    public Texture2D Texture2DRight;
    public Texture2D Texture2DLeft;
    public Texture2D Texture2DTop;
    public Texture2D Texture2DBottom;
    public Texture2D Texture2DFront;
    public Texture2D Texture2DBack;
    public Button[] ButGroup;
    [Header("DOTweening")]
    private float punchCD = .2f;
    private float lastPunchtime = 0;
    public static bool hasResult;
    public Vector3 originalScale;
    Vector3 originalPos;
    public Ease ease;
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
    private Dictionary<NormalVector, Button> butDic;
    Button butcor;
    private void Start()
    {
        originalPos.y = transform.position.y;
        TryGetComponent<Rigidbody>(out rb);
        RewindTime.notOnPlace += ButDicReappear;
        faceTex = new Dictionary<NormalVector, Texture2D>()
        {
            {NormalVector.right,Texture2DRight },
            {NormalVector.left,Texture2DLeft },
            {NormalVector.top,Texture2DTop },
            {NormalVector.bottom,Texture2DBottom },
            {NormalVector.front,Texture2DFront },
            {NormalVector.back, Texture2DBack }
        };
        butDic = new Dictionary<NormalVector, Button>()
        {
            {NormalVector.right, ButGroup[0]},
            {NormalVector.left, ButGroup[1]},
            {NormalVector.top, ButGroup[2]},
            {NormalVector.bottom, ButGroup[3]},
            {NormalVector.front, ButGroup[4]},
            {NormalVector.back, ButGroup[5]},
        };

        thirdPerson.gameObject.SetActive(false);

    }

    public void ButDicDisappear()
    {
        Debug.Log("ButDic");
        foreach (var button in butDic.Values)
        {
            button.transform.DOScale(0, 0.5f).SetEase(Ease.OutBounce);
        }
    }
    void ButDicReappear()
    {
        foreach (var button in butDic.Values)
        {
            button.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
        }
    }
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

        NormalVector face = GetColliderFace(hitNormal, transform); 
        Debug.Log(face);
        Vector3 faceDirection = GetFaceDirection(face);
        butcor = GetButton(face);

        BtnReact();
        VFXImpactTex(faceDirection, face);
        CameraShakeEvent.TriggerShake(1, .25f);
    }

    void BtnReact()
    {
        if (butcor != null)
        {
            if (Time.time - lastPunchtime < punchCD) return;

            lastPunchtime = Time.time;
            originalScale = butcor.transform.localScale;
            Debug.Log(butcor.gameObject.name);
            butcor.transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1), .2f, 1).SetEase(ease).OnComplete(() => butcor.transform.DOScale(originalScale, .01f));
        }
    }
    void VFXImpactTex(Vector3 fDirection, NormalVector f)
    {
        Vector3 centerOfFace = transform.position + (fDirection * transform.lossyScale.magnitude / 2f);
        if (vfx.gameObject.TryGetComponent<VisualEffect>(out VisualEffect vfxCom))
        {
            Texture2D finalTexture2D = GetTexture2D(f);
            vfxCom.SetTexture("MainTex", finalTexture2D);
        }
        else
        {
            Debug.Log("null vfx");
        }
        ObjctPlTrnsfrm.SpawnObject(vfx.gameObject, centerOfFace, Quaternion.identity);
    }
    private Button GetButton(NormalVector result)
    {
        return butDic.TryGetValue(result, out Button butcors) ? butcors : null;
    }
    private Texture2D GetTexture2D(NormalVector face)
    {
        return faceTex.TryGetValue(face, out Texture2D texture2D) ? texture2D : null;
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
    bool isAboveThreshold()
    {
        return transform.position.y > originalPos.y - 2;
    }
    void Update()
    {
        if (rb.linearVelocity.magnitude == 0 && !hasResult)
        {
            if (!isAboveThreshold())
            {
                hasResult = true;
                thirdPerson.gameObject.SetActive(false);
                NormalVector face = GetColliderFace(hitNormal, transform);
               ButDicDisappear();
                Debug.Log(GetFaceDirection(face));
                disCtrlr.UpdatePara(face);
                panel.SetActive(true);
                followWHo.Assigned();
                Debug.Log(followWHo.gameObject.transform);
            }
        }
    }

    private void DrawArrow(Vector3 start, Vector3 direction, Color color)
    {
        Vector3 end = start + direction * 1.5f;
        Debug.DrawLine(start, end, color);
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 150, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -150, 0) * Vector3.forward;
        Debug.DrawLine(end, end + right * 0.3f, color);
        Debug.DrawLine(end, end + left * 0.3f, color);
    }

    private void OnApplicationQuit()
    {
        RewindTime.notOnPlace -= ButDicReappear;
    }
}

