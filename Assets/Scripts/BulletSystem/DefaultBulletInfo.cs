using System;
using System.Collections.Generic;
using UnityEngine;

namespace BulletSystem
{
    [Serializable]
    public struct InfoItem
    {
        [SerializeField]public BulletTag tag;
        [SerializeField]public TagInfo info;
    }
    [CreateAssetMenu(fileName = "DefaultBulletInfo", menuName = "DefaultBulletInfo", order = 0)]
    public class DefaultBulletInfo : ScriptableObject
    {
        public List<InfoItem> defaultInfoItems;
    }
}