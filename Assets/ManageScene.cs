using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{

    //for recording position
    public static bool isPlaying = false;
    void Start()
    {
        isPlaying = false;
    }

    private void FixedUpdate()
    {
        
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    
}
