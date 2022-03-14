using CardSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Control.ActiveCardControl.ControlTypes;
using System.Collections.Generic;
using System.Linq;
using BulletSystem;
using Utils;
using Control;

namespace ActiveCardImplementation
{
    public class ArcParry : ActiveCard, ITappable
    {
        public float tapTime { get; set; }
        public float slowTapTime { get; set; }
        public float pressDuration { get; set; }
        public float releaseDuration { get; set; }
        [SerializeField] public int angle;
        [SerializeField] public int cost;
        private MultiPurposeCollider utilCollider;
        private List<Bullet> bullets;
        private Transform youmuTransform;

        private void Start()
        {
            tapTime = 0.5f;
            slowTapTime = 2;
            pressDuration = 2;
            releaseDuration = 2;
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
                bul.onBulletParry();
            }
        }
    }
}