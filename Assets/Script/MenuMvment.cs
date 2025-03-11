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
    [SerializeField]float duration;
    [SerializeField] private Ease ease;
    // Start is called before the first frame update
    void Start()
    {
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
    void RunSequence()
    {
        ++target_Index;
        if(target_Index == shufTar.Count)
        {
            ShuffleTarg();
            target_Index = 0;
        }
        
        var seq = DOTween.Sequence();
        seq.Append(transform.DOMove(shufTar[target_Index].position, duration).SetEase(ease));
        seq.OnComplete(RunSequence);
    }

}
