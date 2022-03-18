using UnityEngine;

namespace BossBehaviour
{
    [CreateAssetMenu(fileName = "BossAsset", menuName = "BossAsset", order = 0)]
    public class BossAsset : ScriptableObject
    {
        public string characterID;
        public int spellCount;
        
    }
}