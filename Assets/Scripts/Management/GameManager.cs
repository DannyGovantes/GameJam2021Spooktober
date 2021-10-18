using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RatboyStudios.GGJ.Player;

public class GameManager : Singleton<GameManager>
{

    private PlayerController _playerController;
    public PlayerController PlayerController => _playerController;
    
    private void Start()
    {

        _playerController = FindObjectOfType<PlayerController>();
        _playerController.StartCharacter();
        SwitchScheme(false);
    }
    public void SwitchScheme(bool isMenuOpen)
    {
        if (isMenuOpen)
        {
            _playerController.EnableMenuControls();
        }
        else
        {
            _playerController.EnableGameplayControls();
        }
    }
}
