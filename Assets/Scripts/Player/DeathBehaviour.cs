using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RatboyStudios.GGJ.Player
{
    public class DeathBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            animator.GetComponent<PlayerController>().Respawn();

        }
    }
}


