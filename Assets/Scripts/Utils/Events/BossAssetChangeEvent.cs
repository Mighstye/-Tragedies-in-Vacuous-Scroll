using System.Collections.Generic;
using BossBehaviour;
using UnityEngine;

namespace Utils.Events
{
    [CreateAssetMenu(fileName = "BossAssetChange", menuName = "Events/Boss Asset Change (BossAsset->void)", order = 0)]
    public class BossAssetChangeEvent : GenericEvent<BossAsset>
    {
    }
}