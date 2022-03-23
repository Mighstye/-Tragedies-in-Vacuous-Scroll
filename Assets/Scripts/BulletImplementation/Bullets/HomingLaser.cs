using System.Collections.Generic;
using BulletSystem;
using Control;
using UnityEngine;

namespace BulletImplementation
{
    public class HomingLaser : Bullet, ISimpleLaser
    {
        public float homingTime;

        public float transitionTime;

        public float laserTime;

        public float laserWidth;

        public BulletLauncher launcher;
        private EdgeCollider2D col;
        private Vector3 direction;
        private Vector3 endPos;
        private float homingTimer;

        private LineRenderer laserLine;
        private float laserTimer;

        private Vector3 startPos;
        private float transitionTimer;


        public void Launch()
        {
            ResetBullet();
            grazeable = false;

            homingTimer = homingTime;
            laserTimer = laserTime;
            transitionTimer = transitionTime;

            laserLine = GetComponent<LineRenderer>();
            laserLine.startWidth = laserWidth;
            laserLine.endWidth = laserWidth;

            col = GetComponent<EdgeCollider2D>();
            col.enabled = false;
        }

        protected override void AddBehaviors()
        {
            behaviors.Add(HomingPhase);
            behaviors.Add(Transition);
            behaviors.Add(LaserPhase);
        }

        private bool HomingPhase()
        {
            homingTimer -= Time.deltaTime;

            if (homingTimer <= 0.0f) return true;

            var PlayerPos = YoumuController.instance.transform.position;
            startPos = launcher.transform.position;

            var homingVector = (PlayerPos - startPos).normalized;

            endPos = PlayerPos;
            while (FieldBoundaries.instance.left < PlayerPos.x &&
                   PlayerPos.x < FieldBoundaries.instance.right ||
                   FieldBoundaries.instance.down < PlayerPos.y &&
                   PlayerPos.y < FieldBoundaries.instance.up)
                endPos += homingVector;

            laserLine.SetPosition(0, startPos);
            laserLine.SetPosition(1, endPos);

            transform.position = startPos;

            return false;
        }

        private bool Transition()
        {
            transitionTimer -= Time.deltaTime;

            if (transitionTimer <= 0.0f)
            {
                updateCollider();
                col.enabled = true;
                grazeable = true;
                return true;
            }

            return false;
        }

        private bool LaserPhase()
        {
            laserTimer -= Time.deltaTime;

            if (laserTimer <= 0.0f) onBulletDeathNatural?.Invoke();

            return false;
        }

        private void updateCollider()
        {
            //Mesh _mesh = new Mesh();
            //laserLine.BakeMesh(_mesh, true);

            //List<Vector2> colliderPoints = new List<Vector2>();
            //foreach(Vector3 point in _mesh.vertices)
            //{
            //    colliderPoints.Add(new Vector2(point.x, point.y));
            //}

            var colliderPoints = new List<Vector2>();
            colliderPoints.Add(new Vector2(0, 0));
            colliderPoints.Add(new Vector2(endPos.x - startPos.x, endPos.y - startPos.y));
            col.points = colliderPoints.ToArray();
        }
    }
}