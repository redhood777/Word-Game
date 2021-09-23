using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video_Controller : MonoBehaviour
{
    public string url;
    VideoPlayer vidPlayer;
    // Start is called before the first frame update
    void Start()
    {
        vidPlayer = this.GetComponent<VideoPlayer>();
        vidPlayer.url = url;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
