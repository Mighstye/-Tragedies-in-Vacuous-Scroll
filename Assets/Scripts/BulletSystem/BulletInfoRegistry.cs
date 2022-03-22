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
    }
    public class BulletInfoRegistry: Singleton<BulletInfoRegistry>
    {
        private Dictionary<BulletTag, TagInfo> infoRegistry;
        [SerializeField] private DefaultBulletInfo defaultBulletInfo;

        private void Start()
        {
            infoRegistry = new Dictionary<BulletTag, TagInfo>();
            foreach (var item in defaultBulletInfo.defaultInfoItems)
            {
                infoRegistry.Add(item.tag,item.info);
            }
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