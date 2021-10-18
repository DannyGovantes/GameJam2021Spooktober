using System;
using System.Collections;
using System.Collections.Generic;
using RatboyStudios.GGJ.Player;
using UnityEngine;

public class ActionBehaviour : StateMachineBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerController.IsJumpPressed = false;
    }
    
}
