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
                pool.instantiateBulletParry += InstantiateBeamParry;
            }
        }

        private void InstantiateBeamParry(Bullet b)
        {
            b.onBulletParry += () =>
            {
                //Vector3 velocity = b.;
                Vector3 position = b.transform.position;
                b.onBulletDeathNatural.Invoke();

                var bullet = bulletParriedPool.pool.Get();
                ((StandardStraightParry)bullet).Launch(position, Vector3.zero);
            };
        }
    } 
}
