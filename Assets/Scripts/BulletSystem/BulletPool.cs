using UnityEngine;
using UnityEngine.Pool;

namespace BulletSystem
{
    public class BulletPool : MonoBehaviour
    {
        public int maxPoolSize;

        public Bullet bullet;

        public enum PoolType
        {
            Stack,
            LinkedList
        }

        public PoolType poolType;

        // Collection checks will throw errors if we try to release an item that is already in the pool.
        public bool collectionChecks = true;

        private IObjectPool<Bullet> mPool;

        public IObjectPool<Bullet> pool
        {
            get
            {
                if (mPool != null) return mPool;
                if (poolType == PoolType.Stack)
                    mPool = new ObjectPool<Bullet>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                else
                    mPool = new LinkedPool<Bullet>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
                return mPool;
            }
        }

        private Bullet CreatePooledItem()
        {
            var b = Instantiate(bullet,transform);
            b.onBulletDeathNatural += () =>
            {
                pool.Release(b);
            };
            return b;
        }

        // Called when an item is returned to the pool using Release
        private static void OnReturnedToPool(Bullet o)
        {
            o.gameObject.SetActive(false);
        }

        // Called when an item is taken from the pool using Get
        private static void OnTakeFromPool(Bullet o)
        {
            o.gameObject.SetActive(true);
            o.ResetBullet();
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        private static void OnDestroyPoolObject(Bullet o)
        {
            Destroy(o.gameObject);
        }
    }
}
