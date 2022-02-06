using UnityEngine;
using UnityEngine.Pool;

namespace BulletSystem
{

    // This component returns the particle system to the pool when the OnParticleSystemStopped event is received.
    [RequireComponent(typeof(GameObject))]
    public class ReturnToPool : MonoBehaviour
    {
        public GameObject o;
        public IObjectPool<GameObject> pool;

        void Start()
        {
            //o = GetComponent<GameObject>();
            //var main = system.main;
            //main.stopAction = ParticleSystemStopAction.Callback;
        }

        void onBulletDeathNatural()
        {
            // Return to the pool
            pool.Release(o);
        }
    }

    // This example spans a random number of ParticleSystems using a pool so that old systems can be reused.
    public class BulletPool : MonoBehaviour
    {
        public int maxPoolSize;

        public GameObject bullet;

        public enum PoolType
        {
            Stack,
            LinkedList
        }

        public PoolType poolType;

        // Collection checks will throw errors if we try to release an item that is already in the pool.
        public bool collectionChecks = true;

        IObjectPool<GameObject> m_Pool;

        public IObjectPool<GameObject> Pool
        {
            get
            {
                if (m_Pool == null)
                {
                    if (poolType == PoolType.Stack)
                        m_Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                    else
                        m_Pool = new LinkedPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
                }
                return m_Pool;
            }
        }

        GameObject CreatePooledItem()
        {
            var b = Instantiate(bullet);

            var returnToPool = b.AddComponent<ReturnToPool>();
            returnToPool.o = b;
            returnToPool.pool = Pool;

            return b;
        }

        // Called when an item is returned to the pool using Release
        void OnReturnedToPool(GameObject o)
        {
            o.gameObject.SetActive(false);
        }

        // Called when an item is taken from the pool using Get
        void OnTakeFromPool(GameObject o)
        {
            o.gameObject.SetActive(true);
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        void OnDestroyPoolObject(GameObject o)
        {
            Destroy(o.gameObject);
        }
    }
}
