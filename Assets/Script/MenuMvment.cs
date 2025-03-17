using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
public class MenuMvment : MonoBehaviour
{
    [SerializeField] List<Transform> targets;
    private List<Transform> shufTar;
    int target_Index = -1;
    [SerializeField] float duration;
    [SerializeField] private Ease ease;
    Sequence seq;
    // Start is called before the first frame update
    void Start()
    {
        RewindTime.onPlace += DelSeq;
        ShuffleTarg();
        RunSequence();
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
        yield return new WaitForSeconds(1);
       RunSequence();
    }
    void RunSequence()
    {
        ++target_Index;
        if (target_Index == shufTar.Count)
        {
            ShuffleTarg();
            target_Index = 0;
        }

        seq = DOTween.Sequence();
     /*   seq.AppendInterval(1);*/
        seq.Append(transform.DOMove(shufTar[target_Index].position + new Vector3(0, 1, 0), duration).SetEase(ease));
        seq.Join(transform.DORotate(RandRot(), duration).SetEase(ease));
        seq.OnComplete(RunSequence);
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
