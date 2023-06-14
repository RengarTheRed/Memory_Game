using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public float timerDuration = 60f;
    public TMP_Text timerText;
    
    private bool _bTimerActive = true;
    private float _currTimer = 0f;

    //UI Elements
    public Button restartButton;
    public Button quitButton;
    public RectTransform overlayPanel;
    public TMP_Text gameOverText;

    void Start()
    {
        _currTimer = timerDuration;
        
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
        if (_bTimerActive && Time.timeScale!=0)
        {
            _currTimer -= Time.deltaTime;
            if (_currTimer < 0)
            {
                _bTimerActive = false;
                TimerEnded();
            }
            timerText.SetText(_currTimer.ToString("n0"));
        }
    }

    private void PauseGame()
    {
        if (Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        overlayPanel.gameObject.SetActive(!overlayPanel.gameObject.activeSelf);
    }
    
    //Function accessed when timer ends
    private void TimerEnded()
    {
        GameOver(false);
    }

    //Game over function true = win false = lose. Just shows text
    public void GameOver(bool bWin)
    {
        //Disables timer and shows text
        gameOverText.gameObject.SetActive(true);
        _bTimerActive = false;
        if (bWin)
        {
            gameOverText.SetText("YOU WIN!");
        }
        else
        {
            gameOverText.SetText("YOU LOSE!");
        }
    }

    //UI Restart and Quit Buttons
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
