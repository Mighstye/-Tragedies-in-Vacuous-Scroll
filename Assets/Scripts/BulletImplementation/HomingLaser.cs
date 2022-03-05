using System.Collections.Generic;
using UnityEngine;
using BulletSystem;
using Control;

namespace BulletImplementation
{
    public class HomingLaser : Bullet, ISimpleLaser
    {
        private Vector3 direction;

        public float homingTime;
        private float homingTimer;

        public float transitionTime;
        private float transitionTimer;

        public float laserTime;
        private float laserTimer;

        public float laserWidth;

        private Vector3 startPos;
        private Vector3 endPos;

        private LineRenderer laserLine;
        private EdgeCollider2D col;

        public BulletLauncher launcher;

        public override Vector3 getVelocity()
        {
            return direction;
        }

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

            if (homingTimer <= 0.0f)
            {
                return true;
            }

            var PlayerPos = YoumuController.instance.transform.position;
            startPos = launcher.transform.position;

            var homingVector = (PlayerPos - startPos).normalized;

            endPos = PlayerPos;
            while(FieldBoundaries.instance.left < PlayerPos.x && 
                   PlayerPos.x < FieldBoundaries.instance.right||
                  FieldBoundaries.instance.down < PlayerPos.y && 
                  PlayerPos.y < FieldBoundaries.instance.up)
            {
                endPos += homingVector;
            }

            laserLine.SetPosition(0, startPos);
            laserLine.SetPosition(1, endPos);

            this.transform.position = startPos;

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

            if (laserTimer <= 0.0f)
            {
                onBulletDeathNatural?.Invoke();
            }

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

            List<Vector2> colliderPoints = new List<Vector2>();
            colliderPoints.Add(new Vector2(0, 0));
            colliderPoints.Add(new Vector2(endPos.x - startPos.x, endPos.y - startPos.y));
            col.points = colliderPoints.ToArray();
        }


    }
}