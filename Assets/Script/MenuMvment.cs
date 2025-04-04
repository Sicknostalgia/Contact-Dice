using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using Ami.BroAudio;
public class MenuMvment : MonoBehaviour
{
    [SerializeField] SoundID boinkSnd;
    [SerializeField] List<Transform> targets;
    private List<Transform> shufTar;
    int target_Index = -1;
    [SerializeField] float duration;
    [SerializeField] private Ease ease;
    Sequence seq;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        RewindTime.onPlace += DelSeq;
        rb = GetComponent<Rigidbody>();
        ShuffleTarg();
        StartCoroutine(DelaySeq());
    }

    void ShuffleTarg()
    {
        shufTar = new List<Transform>(targets); //public List(IEnumerable<T> collection);
        for (int i = 0; i < shufTar.Count; i++)
        {
            int rndmIndx = Random.Range(i, shufTar.Count);
            // tuple deconstruction C#7.0
            (shufTar[i], shufTar[rndmIndx]) = (shufTar[rndmIndx], shufTar[i]);
        }
    }

    Vector3 RandRot() //method return type
    {
        return new Vector3(
            Random.Range(0, 360),
            Random.Range(0, 360),
            Random.Range(0, 360)
            );
    }
    void DelSeq()
    {
        StartCoroutine(DelaySeq());
    }
    IEnumerator DelaySeq()
    {
        rb.isKinematic = true;
        yield return new WaitForSeconds(1);
       RunSequence();
        rb.isKinematic = false;
    }
    public void RunSequence()
    {
        ++target_Index;
        if (target_Index == shufTar.Count)
        {
            ShuffleTarg();
            target_Index = 0;
        }

        seq = DOTween.Sequence();
     /*   seq.AppendInterval(1);*/
        seq.Append(transform.DOMove(shufTar[target_Index].position + new Vector3(0, 1, 0), duration).SetEase(ease)).OnStepComplete(() => OnReachedTarget(target_Index));
        seq.Join(transform.DORotate(RandRot(), duration).SetEase(ease));
        seq.OnComplete(RunSequence);
    }
    void OnReachedTarget(int index)  //when player traverse to specific words in menu
    {
        Debug.Log("Reached target index: " + index);

        // Get the child of the target transform
        Transform child = shufTar[index].GetChild(0); // Assuming it has at least one child
        if (child == null) return;

        // Apply a shake effect to the child (glitch effect)
        child.DOShakePosition(0.5f, strength: 0.2f, vibrato: 20, randomness: 90);
        child.DOShakeRotation(0.5f, strength: new Vector3(0, 0, 10), vibrato: 10, randomness: 90);
        child.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1); // Quick scale punch
        BroAudio.Play(boinkSnd);
    }

    public void StopSeq()
    {
        seq.Kill();
    }
    private void OnApplicationQuit()
    {
        RewindTime.onPlace -= DelSeq;
    }

}
