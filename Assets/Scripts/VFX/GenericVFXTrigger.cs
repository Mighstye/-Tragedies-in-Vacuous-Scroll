using BossBehaviour;
using UnityEngine;

namespace VFX
{
    public class GenericVFXTrigger : MonoBehaviour
    {
        public void SetToBossPosition()
        {
            transform.position = BossBehaviourSystemProxy.instance.bossController.transform.position;
        }
    }
}