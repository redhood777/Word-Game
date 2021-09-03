using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class introSceneChange : MonoBehaviour
{
    public int time;
    private void Awake()
    {
        Invoke("changescenetomain", time);
    }
    void changescenetomain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
