using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;
using BulletSystem;
using BulletImplementation;

namespace PassiveCardImplementation
{
    public class BeamParry : PassiveCard
    {
        [SerializeField] private Transform bulletPoolsContainer;

        [SerializeField]  private BulletPool bulletParriedPool;

        void Start()
        {
            // Modification du comportement des Bullet Pools

            // Recuperation des bullet pools
            List<BulletPool> bulletPools = new List<BulletPool>();

            var pools = bulletPoolsContainer.gameObject.GetComponentsInChildren<BulletPool>();
            foreach (var pool in pools)
            {
                pool.instantiateBulletParry += instantiateBeamParry;
            }
        }

        private void instantiateBeamParry(Bullet b)
        {
            b.onBulletPary += () =>
            {
                Vector3 velocity = b.getVelocity();
                Vector3 position = b.transform.position;
                b?.onBulletDeathNatural.Invoke();

                var bullet = bulletParriedPool.pool.Get();
                ((StandardStraightPary)bullet).Launch(position, -velocity);
            };
        }
    } 
}
