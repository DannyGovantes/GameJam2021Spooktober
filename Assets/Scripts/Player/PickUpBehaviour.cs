﻿using System.Collections;
using System.Collections.Generic;
using RatboyStudios.GGJ.Player;
using UnityEngine;

public class PickUpBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.GetComponent<PlayerController>();
        //if(player) player.CallPickUpAction();
    }

}
