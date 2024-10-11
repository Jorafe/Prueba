using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int stars = 0;

    private bool isPaused;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
        }

        SoundManager.instance.PlaySFX(SoundManager.instance._audioSource, SoundManager.instance._StopAudio);
    }


    public void AddStars()
    {
        stars++;
    }
   
}
