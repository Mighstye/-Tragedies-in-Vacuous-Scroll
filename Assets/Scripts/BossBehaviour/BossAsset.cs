using UnityEngine;

namespace BossBehaviour
{
    [CreateAssetMenu(fileName = "BossAsset", menuName = "Boss Asset", order = 0)]
    public class BossAsset : ScriptableObject
    {
        public string characterID;
        public int spellCount;
        public GameObject bossPrefab;
        public Sprite spellSprite;
        public Sprite background;
    }
}