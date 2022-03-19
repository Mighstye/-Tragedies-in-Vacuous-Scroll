using System;
using CardSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Control.ActiveCardControl.ControlTypes;
using System.Collections.Generic;
using System.Linq;
using BulletImplementation;
using BulletSystem;
using Utils;
using Control;
using UnityEditor;

namespace ActiveCardImplementation
{
    public class ArcParry : ActiveCard, ITappable
    {
        public float tapTime { get; set; }
        [SerializeField] public int angle;
        [SerializeField] public int cost;
        [SerializeField] private BulletPool bulletParriedPool;
        private MultiPurposeCollider utilCollider;
        private List<Bullet> bullets;
        private Transform youmuTransform;

        private void Start()
        {
            tapTime = 0.5f;
            grazeCostSegment = cost;
            utilCollider = GameObject.Find("MultiPurposeCollider").GetComponent<MultiPurposeCollider>();
        }

        public void OnTapPerformed(InputAction.CallbackContext context)
        {
            if (useCard() == true) Parry();
        }

        public void OnTapCancelled(InputAction.CallbackContext context)
        {
            return;
        }

        private void Parry()
        {
            bullets = new List<Bullet>(utilCollider.Get());
            youmuTransform = YoumuController.instance.transform;
            foreach (var bul in from bul in bullets 
                     let angleBetween = Vector2.Angle((Vector2)youmuTransform.position,
                         (Vector2)bul.transform.position) where angleBetween <= angle && 
                                                                bul.transform.position.y > youmuTransform.position.y select bul)
            {
                ParryBullet(bul);
                bul.InvokeBulletParry();
                
            }
        }

        private void ParryBullet(Bullet bullet)
        {
            if (bullet.bulletTags.Any(bulletTag => !BulletInfoRegistry.instance.GetInfo(bulletTag).canBeParried)) return;
            var position = bullet.transform.position;
            bullet.InvokeBulletDeath();
            var counterBullet = bulletParriedPool.pool.Get();
            ((StandardStraightParry)counterBullet).Launch(position, Vector3.zero);
        }

        private void OnDrawGizmos()
        {
            if (!EditorApplication.isPlaying) return;
            Handles.DrawWireArc(YoumuController.instance.transform.position,Vector3.forward,
                Quaternion.AngleAxis(-angle,Vector3.forward)*Vector3.up,angle*2,2f,0.2f);
        }
    }
}