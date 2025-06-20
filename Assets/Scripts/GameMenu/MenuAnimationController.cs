using UnityEngine;

namespace GameBlockAdv.GameMenu
{
    public class MenuAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        public void PlayAnimation(string triggerName)
        {
            animator.SetTrigger(triggerName);
        }
    }
}

