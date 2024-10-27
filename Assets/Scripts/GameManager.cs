using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int stars = 0;

    private int coins = 0;

    [SerializeField] Text _starsText;

    [SerializeField] Text _coinsText;

    private Animator _pauseMenuAnim;

    private bool isPaused;

    private bool isVictory;

    private bool pauseAnimation;

    [SerializeField] private GameObject _pauseMenu;

    [SerializeField] private GameObject _victoryMenu;
    
    [SerializeField] private GameObject _optionsMenu;

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

    
      

    public void Victory()
    {
        
        if (!isVictory && !pauseAnimation)
        {   
            isVictory = true;
            Time.timeScale = 0;
            _victoryMenu.SetActive(true);
            SoundManager.instance.PlaySFX(SoundManager.instance._audioSource,SoundManager.instance._VictoryAudio);

        }
        
    }    

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(LoadAsync("Main Menu"));
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


    public void AddStars()
    {
        HUD.instance.ActivarEstrellas(stars);
        stars += 1;
        if(stars >= 3)
            {
                GameManager.instance.Victory();  
            }
    }

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
         Time.timeScale = 1;
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    

   
}
