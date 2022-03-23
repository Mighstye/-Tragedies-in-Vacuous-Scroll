using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace BulletSystem
{
    [Serializable]
    public struct ConstantPoolListItem
    {
        [SerializeField] public string key;
        [SerializeField] public BulletPool pool;
    }

    public class ConstantPoolList : Singleton<ConstantPoolList>
    {
        [SerializeField] private List<ConstantPoolListItem> pools;

        public BulletPool GetPool(string key)
        {
            return pools.FirstOrDefault(item => item.key == key).pool;
        }
    }
}