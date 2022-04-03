using System.Collections.Generic;
using BulletSystem;
using Control;
using UnityEngine;

namespace BulletImplementation
{
    public class HomingLaserCue : Bullet, ISimpleLaser
    {
        public float homingTime;

        public float laserWidth;

        public BulletLauncher launcher;
        public Vector3 endPos { get; private set; }
        public Vector3 startPos { get; private set; }
        private float homingTimer;

        private LineRenderer laserCueLine;

        public bool isAimed { get; private set; }

        public void Launch()
        {
            ResetBullet();
            grazeable = false;
            isAimed = false;

            homingTimer = homingTime;

            laserCueLine = GetComponent<LineRenderer>();
            laserCueLine.startWidth = laserWidth;
            laserCueLine.endWidth = laserWidth;
        }

        protected override void AddBehaviors()
        {
            behaviors.Add(HomingPhase);
            behaviors.Add(AimingPhase);
        }

        private bool HomingPhase()
        {
            homingTimer -= Time.deltaTime;

            if (homingTimer <= 0.0f) return true;

            var PlayerPos = YoumuController.instance.transform.position;
            startPos = launcher.transform.position;

            var homingVector = (PlayerPos - startPos).normalized;

            endPos = PlayerPos;
            while ((FieldBoundaries.instance.left < endPos.x &&
                   endPos.x < FieldBoundaries.instance.right) &&
                   (FieldBoundaries.instance.down < endPos.y &&
                   endPos.y < FieldBoundaries.instance.up))
                endPos += homingVector;

            laserCueLine.SetPosition(0, startPos);
            laserCueLine.SetPosition(1, endPos);

            transform.position = startPos;

            return false;
        }

        private bool AimingPhase()
        {
            if (!isAimed)
            {
                isAimed = true;
            }
            return false;
        }
    }
}
