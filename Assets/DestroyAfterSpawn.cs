using UnityEngine;
using System.Collections;
using System;
public class DestroyAfterSpawn : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(TickTime());
    }

    IEnumerator TickTime()
    {
        yield return new WaitForSeconds(1);
        ObjctPlTrnsfrm.ReturnObjectToPool(gameObject);
    }
}
