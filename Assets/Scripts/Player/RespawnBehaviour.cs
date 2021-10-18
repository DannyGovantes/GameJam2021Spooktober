using UnityEngine;

namespace RatboyStudios.GGJ.Player
{
    public class RespawnBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<PlayerController>().RespawnFinished();
        }
    }
}
