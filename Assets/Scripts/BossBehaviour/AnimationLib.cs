using UnityEngine;

namespace BossBehaviour
{
    public abstract class AnimationLib
    {
        public Animator animator;
        public abstract void AnimationMove(float direction);
        public abstract void AnimationAttack();
    }
}