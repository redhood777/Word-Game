using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSound : MonoBehaviour
{
    bool sound;
    public AudioSource[] sources;

    public Text soundText;

    private void Start()
    {
        sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
      
        if(sound)
        {
            soundText.text = "Sound ON";
        }
        else
        {
            soundText.text = "Sound OFF";
        }
       
    }

    // Start is called before the first frame update
    public void SoundON_OFF()
    {
        sound = !sound;

        if(sound)
        {
            sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource music in sources)
            {
                music.mute = true;
                // Debug.Log("Sound On");
                soundText.text = "Sound ON";
            }

        }
        else
        {
            sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource music in sources)
            {
                music.mute = false;
                // Debug.Log("Sound On");
                soundText.text = "Sound OFF";
            }

        }
    }
}
