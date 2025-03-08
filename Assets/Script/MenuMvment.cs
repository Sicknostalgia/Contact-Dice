using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MenuMvment : MonoBehaviour
{
    private List<Transform> targets;
    int target_Index = -1;
    [SerializeField]float duration;
    [SerializeField] private Ease ease;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void RunSequence()
    {
        ++target_Index;

        if(target_Index == targets.Count)
        {
            target_Index = 0;
        }
        var seq = DOTween.Sequence();

        seq.Append(transform.DOMove(targets[target_Index].position, duration).SetEase(ease));
        seq.OnComplete(RunSequence);
    }

}
