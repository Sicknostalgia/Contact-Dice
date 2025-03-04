using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    bool isRewinding= false;
    //for recording position
    List<Vector3> positions;

    void Start()
    {
        positions = new List<Vector3>();
    }

    private void FixedUpdate()
    {
        
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    public void StartRewind()
    {
        isRewinding = true;
    }
    public void StopRewind()
    {
        isRewinding = false;
    }
}
