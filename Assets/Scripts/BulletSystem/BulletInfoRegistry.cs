using System;
using System.Collections.Generic;
using UnityEngine;

namespace BulletSystem
{
    [Serializable]
    public struct TagInfo
    {
        [SerializeField] public bool canBeParried;
    }
    public class BulletInfoRegistry:MonoBehaviour
    {
        public static BulletInfoRegistry instance { get; private set; }
        private Dictionary<BulletTag, TagInfo> infoRegistry;
        [SerializeField] private DefaultBulletInfo defaultBulletInfo;
        
        private void Awake()
        {
            instance = this;
        }

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