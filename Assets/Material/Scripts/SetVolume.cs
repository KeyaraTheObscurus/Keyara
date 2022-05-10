using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour {

    public AudioMixer Master;

    public void SetLevel (float sliderValue)
    {
        Master.SetFloat ("MasterVolume", Mathf.Log10 (sliderValue) * 20 );
    }

    void Update ()
    {
                if (Input.GetKeyDown("["))
        {
            Master.SetFloat ("MasterVolume", -80f);
            print ("Mute");
        }
            if (Input.GetKeyDown("]"))
        {
            Master.SetFloat ("MasterVolume", 0.0f);
            print ("Unmute");
        }
    }
}
