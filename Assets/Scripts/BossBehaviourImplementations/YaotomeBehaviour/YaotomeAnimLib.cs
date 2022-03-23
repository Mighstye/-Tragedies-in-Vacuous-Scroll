using BossBehaviour;
using UnityEngine;

namespace YaotomeBehaviour
{
    public class YaotomeAnimLib : AnimationLib
    {
        private static readonly int Direction = Animator.StringToHash("Direction");

        public override void AnimationMove(float direction)
        {
            animator.SetFloat(Direction, direction);
        }

        public override void AnimationAttack()
        {
        }
    }
}