using BossBehaviour;
using UnityEngine;

namespace Utils.Events
{
    [CreateAssetMenu(fileName = "BossInstantiate", menuName = "Events/Boss Instantiate (BossController->void)", order = 0)]
    public class BossInstantiateEvent: GenericEvent<BossController>
    {
        
    }
}