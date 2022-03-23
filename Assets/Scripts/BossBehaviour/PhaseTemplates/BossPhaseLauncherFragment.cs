using System.Collections.Generic;
using UnityEngine;

namespace BossBehaviour
{
    public abstract class BossPhaseLauncherFragment : BossPhaseFragment
    {
        private static readonly int End = Animator.StringToHash("fragmentEndLauncher");
        [SerializeField] private List<int> launcherIndexNumbers;
        [SerializeField] private bool disableLaunchersOnExit = true;

        private bool fragmentEnd;
        private LauncherIndex launcherIndex;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            fragmentEnd = false;
            launcherIndex = animator.GetComponent<LauncherIndex>();
            animator.SetBool(End, false);
            foreach (var id in launcherIndexNumbers) launcherIndex.launchers[id].gameObject.SetActive(true);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!disableLaunchersOnExit) return;
            foreach (var id in launcherIndexNumbers) launcherIndex.launchers[id].gameObject.SetActive(false);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (fragmentEnd) return;
            if (!FragmentEnd()) return;
            fragmentEnd = true;
            animator.SetBool(End, true);
        }

        protected virtual bool FragmentEnd()
        {
            return false;
        }
    }
}