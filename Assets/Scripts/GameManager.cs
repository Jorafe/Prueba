using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int stars = 0;

    private int coins = 0;

    [SerializeField] Text _starsText;

    [SerializeField] Text _coinsText;

    private Animator _pauseMenuAnim;

    private bool isPaused;

    public HUD hud;

    private bool pauseAnimation;

    [SerializeField] private GameObject _pauseMenu;

    [SerializeField] private Slider _healthslider;   

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
        
        if (!isPaused && !pauseAnimation)
        {   
            isPaused = true;
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }
        else if(isPaused && !pauseAnimation) 
        {
            pauseAnimation = true;

            StartCoroutine(ClosePauseAnimation());
        }     

        
    }

    IEnumerator ClosePauseAnimation()
    {
        _pauseMenuAnim.SetBool("Close", true);

        yield return new WaitForSecondsRealtime(0.30f);

        Time.timeScale = 1;
        isPaused = false;
        _pauseMenu.SetActive(false);

        pauseAnimation = false;
        
    }


    /*public void AddStars()
    {
        stars++;
        _starsText.text = stars.ToString();
        
    }*/

     public void AddCoins()
    {
        coins++;
        _coinsText.text = coins.ToString();
    }

    public void SetHealthSlider(int maxHealth)
    {
        _healthslider.maxValue = maxHealth;
        _healthslider.value = maxHealth;
    }

    public void UpdateHealthSlider(int health)
    {
        _healthslider.value = health;
    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

   
}
