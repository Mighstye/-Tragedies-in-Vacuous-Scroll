using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace BulletSystem
{
    [Serializable]
    public struct TagInfo
    {
        [SerializeField] public bool canBeParried;
        [SerializeField] public bool canBeWipedByPostHit;
        [SerializeField] public bool canBeWipedBySpell;
    }

    public class BulletInfoRegistry : Singleton<BulletInfoRegistry>
    {
        [SerializeField] private DefaultBulletInfo defaultBulletInfo;
        private Dictionary<BulletTag, TagInfo> infoRegistry;

        private void Start()
        {
            infoRegistry = new Dictionary<BulletTag, TagInfo>();
            foreach (var item in defaultBulletInfo.defaultInfoItems) infoRegistry.Add(item.tag, item.info);
        }

        public TagInfo GetInfo(BulletTag bulletTag)
        {
            return infoRegistry[bulletTag];
        }

        public void UpdateInfo(BulletTag bulletTag, TagInfo tagInfo)
        {
            infoRegistry[bulletTag] = tagInfo;
        }
    }
}