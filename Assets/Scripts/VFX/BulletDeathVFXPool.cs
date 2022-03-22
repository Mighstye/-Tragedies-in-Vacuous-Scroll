using System;
using BulletSystem;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;
using Utils;

namespace VFX
{
    public class BulletDeathVFXPool : Singleton<BulletDeathVFXPool>
    {

        public int maxPoolSize;

        public BulletDeathVFX vfx;

        public enum PoolType
        {
            Stack,
            LinkedList
        }

        public PoolType poolType;

        // Collection checks will throw errors if we try to release an item that is already in the pool.
        public bool collectionChecks = true;

        private IObjectPool<BulletDeathVFX> mPool;

        public IObjectPool<BulletDeathVFX> pool
        {
            get
            {
                if (mPool != null) return mPool;
                if (poolType == PoolType.Stack)
                    mPool = new ObjectPool<BulletDeathVFX>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                else
                    mPool = new LinkedPool<BulletDeathVFX>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
                return mPool;
            }
        }

        private BulletDeathVFX CreatePooledItem()
        {
            var b = Instantiate(vfx,transform);
            b.onEffectEnd += ()=>
            {
                pool.Release(b);
            };
            return b;
        }
        
        private void OnReturnedToPool(BulletDeathVFX o)
        {
            o.enabled = false;
            o.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(BulletDeathVFX o)
        {
            o.gameObject.SetActive(true);
            o.enabled = true;
        }
        
        private void OnDestroyPoolObject(BulletDeathVFX o)
        {
            Destroy(o.gameObject);
        }
    }
}