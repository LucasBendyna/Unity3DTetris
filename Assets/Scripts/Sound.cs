using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    /*
    * Intialize the sound state (muted or unmuted).
    */
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(EditSoundState);

        if (AudioManager.Instance.IsMusicMuted())
        {
            transform.GetChild(1).gameObject.GetComponent<RawImage>().enabled = true;
            transform.GetChild(0).gameObject.GetComponent<RawImage>().enabled = false;
        }
        else
        {
            transform.GetChild(1).gameObject.GetComponent<RawImage>().enabled = false;
            transform.GetChild(0).gameObject.GetComponent<RawImage>().enabled = true;
        }
    }

    /*
    * Change the sound state.
    */
    void EditSoundState()
    {
        AudioManager.Instance.PauseOrResumeMusic();
        transform.GetChild(0).gameObject.GetComponent<RawImage>().enabled = !transform.GetChild(0).gameObject.GetComponent<RawImage>().enabled;
        transform.GetChild(1).gameObject.GetComponent<RawImage>().enabled = !transform.GetChild(1).gameObject.GetComponent<RawImage>().enabled;
    }

}
