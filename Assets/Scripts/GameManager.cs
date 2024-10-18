using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int stars = 0;

    private int coins = 0;

    [SerializeField] Text _starsText;

    [SerializeField] Text _coinsText;

    private Animator _pauseMenuAnim;

    private bool isPaused;

    [SerializeField] private GameObject _pauseMenu;

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

        _pauseMenuAnim = _pauseMenu.GetComponentInChildren<Animator>();
    }

    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            _pauseMenu.SetActive(true);
        }
        else
        {
            StartCoroutine(ClosePauseAnimation());
        }
        
        
           

        SoundManager.instance.PlaySFX(SoundManager.instance._audioSource, SoundManager.instance._StopAudio);
    }

    IEnumerator ClosePauseAnimation()
    {
        _pauseMenuAnim.SetBool("Close", true);

        yield return new WaitForSecondsRealtime(0.30f);

        Time.timeScale = 1;
        isPaused = false;
        _pauseMenu.SetActive(false);
        
    }


    public void AddStars()
    {
        stars++;
        _starsText.text = stars.ToString();
    }

     public void AddCoins()
    {
        coins++;
        _coinsText.text = coins.ToString();
    }
   
}
