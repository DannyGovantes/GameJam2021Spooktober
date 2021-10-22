using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RatboyStudios.GGJ.Player;

public class GameManager : Singleton<GameManager>
{

    private PlayerController _playerController;
    public PlayerController PlayerController => _playerController;
    [SerializeField] private float timeRemaining=100;
    [SerializeField]private bool timerIsRunning=false;
    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerController.StartCharacter();
        SwitchScheme(false);
    }
    public void startTimer(){
        timerIsRunning=true;
    }
    public string  getTimeRemaining(){
        timeRemaining+=1;
        float minutes= Mathf.FloorToInt(timeRemaining/60);
        float seconds= Mathf.FloorToInt(timeRemaining%60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void stopTimer(){
        timerIsRunning=false;
    }
    public void SwitchScheme(bool isMenuOpen)
    {
        if (isMenuOpen)
        {
            stopTimer();
            _playerController.EnableMenuControls();
        }
        else
        {
            startTimer();
            _playerController.EnableGameplayControls();
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                SwitchScheme(false);
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}
