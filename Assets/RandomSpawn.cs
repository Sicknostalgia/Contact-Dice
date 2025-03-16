using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        RandomizePos();
        RewindTime.onPlace += RandomizePos;
    }

    void RandomizePos()
    {
        transform.position = new Vector3(Random.Range(192.58f, 212.4f), 248, Random.Range(90.46f, 101.5f));
    }

    private void OnApplicationQuit()
    {
        RewindTime.onPlace -= RandomizePos;
    }
}
