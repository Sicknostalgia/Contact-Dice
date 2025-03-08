using UnityEngine;
using DG.Tweening; // make sure this is included on all scripts that utilize DOTween!

public class ExampleCubeMovement : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] KeyCode startAnimation1Key;
    [SerializeField] KeyCode startAnimation2Key;
    [SerializeField] KeyCode resetAnimationKey;

    [Header("Animation - Challenge 1")]
    [SerializeField] Vector3 moveTarget;
    [SerializeField] float moveTime;

    [SerializeField] Vector3 rotationTarget;
    [SerializeField] float rotationTime;

    [SerializeField] Vector3 scaleTarget;
    [SerializeField] float scaleTime;

    [Header("Animation - Challenge  2")]
    [SerializeField] Vector3 moveTarget2;
    [SerializeField] float moveTime2;

    [SerializeField] Vector3 rotationTarget2;
    [SerializeField] float rotationTime2;

    [SerializeField] Vector3 scaleTarget2;
    [SerializeField] float scaleTime2;

    [SerializeField] Ease animationEase2 = Ease.OutElastic;

    void PerformMyAnimation1()
    {
        transform.DOMove(moveTarget, moveTime);
        transform.DORotate(rotationTarget, rotationTime);
        transform.DOScale(scaleTarget, scaleTime);
    }

    void PerformMyAnimation2()
    {
        transform.DOMove(moveTarget2, moveTime2).SetEase(animationEase2).SetLoops(-1, loopType:LoopType.Yoyo);
        transform.DORotate(rotationTarget2, rotationTime2).SetEase(animationEase2).SetLoops(-1, loopType: LoopType.Yoyo);
        transform.DOScale(scaleTarget2, scaleTime2).SetEase(animationEase2).SetLoops(-1, loopType: LoopType.Yoyo);
    }

    void Update()
    {
        if (Input.GetKeyDown(startAnimation1Key))
        {
            PerformMyAnimation1();
        }
        else if (Input.GetKeyDown(startAnimation2Key))
        {
            PerformMyAnimation2();
        }
        else if (Input.GetKeyDown(resetAnimationKey))
        {
            ResetAnimations();
        }
    }

    void ResetAnimations()
    {
        transform.DOKill();
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        transform.localScale = Vector3.one;
    }
}

