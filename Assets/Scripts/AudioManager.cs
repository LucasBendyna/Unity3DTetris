using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // The audio source associated with the music.
    public AudioSource music;

    // The audio source associated with the sound effect "TetrominoPlaced".
    public AudioSource tetrominoPlaced;

    // The audio source associated with the sound effect "LineComplete".
    public AudioSource lineComplete;

    // The audio source associated with the sound effect "ButtonClicked".
    public AudioSource buttonClicked;

    // Check if the sound is muted.
    bool isMuted;

    // Instance of AudioManager.
    static AudioManager instance = null;

    /*
    * Get the AudioManager instance.
    * 
    * Return :
    *   AudioManager - The AudioManager instance.
    */
    public static AudioManager Instance
    {
        get { return instance; }
    }

    /*
    * Applay the DontDestroyOnLoad property to the unique AudioManager instance.
    */
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /*
    * Mute or unmute the audio sources.
    */
    public void PauseOrResumeMusic() {
        music.mute = !music.mute;
        tetrominoPlaced.mute = !tetrominoPlaced.mute;
        lineComplete.mute = !lineComplete.mute;
        buttonClicked.mute = !buttonClicked.mute;
        isMuted = music.mute;
    }

    /*
    * Return true if the sound is muted, false instead.
    * 
    * Return :
    *   bool - Value of isMuted.
    */
    public bool IsMusicMuted()
    {
        return isMuted;
    }

}
