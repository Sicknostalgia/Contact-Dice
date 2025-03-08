using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MenuMvment : MonoBehaviour
{
    [SerializeField] Vector3[] vecList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Travel2Vector());
    }

    IEnumerator Travel2Vector()
    {
        for (int i = 0; i < vecList.Length; i++)
        {
            transform.DOMove(vecList[i], 1.5f).SetEase(Ease.OutElastic);
        }
        yield return null;
    }
}
