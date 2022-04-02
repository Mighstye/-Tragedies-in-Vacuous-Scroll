using System.Collections.Generic;
using BulletSystem;
using Control;
using UnityEngine;
using UnityEngine.Events;

namespace BulletImplementation
{
    public class HomingLaser : Bullet, ISimpleLaser
    {
        public float laserTime;

        public float laserWidth;

        public BulletLauncher launcher;
        private EdgeCollider2D col;
        public Vector3 endPos;
        public Vector3 startPos;

        private LineRenderer laserLine;
        private float laserTimer;

        public UnityEvent laserDeath = new UnityEvent();

        public void Launch()
        {
            ResetBullet();
            grazeable = false;

            laserTimer = laserTime;

            laserLine = GetComponent<LineRenderer>();
            laserLine.startWidth = laserWidth;
            laserLine.endWidth = laserWidth;

            laserLine.SetPosition(0, startPos);
            laserLine.SetPosition(1, endPos);

            transform.position = startPos;

            col = GetComponent<EdgeCollider2D>();
            var colliderPoints = new List<Vector2>();
            colliderPoints.Add(new Vector2(0, 0));
            colliderPoints.Add(new Vector2(endPos.x - startPos.x, endPos.y - startPos.y));
            col.points = colliderPoints.ToArray();
            col.enabled = true;
        }

        protected override void AddBehaviors()
        {
            behaviors.Add(LaserPhase);
        }

        private bool LaserPhase()
        {
            laserTimer -= Time.deltaTime;

            if (laserTimer <= 0.0f)
            {
                InvokeBulletDeath();
            }

            return false;
        }
    }
}