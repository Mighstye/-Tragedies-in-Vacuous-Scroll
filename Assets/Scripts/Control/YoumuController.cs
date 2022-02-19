using System;
using System.Collections;
using BulletSystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using Logic_System;

namespace Control
{
    public class YoumuController : MonoBehaviour
    {
        public static YoumuController instance { get; private set; }
        //Move related fields
        private Vector3 moveDirection;
        private float currentSpeed;
        [SerializeField] private float normalSpeed;
        [SerializeField] private float lowSpeed;
        //Sprite and Animation related fields
        private float halfSizeX, halfSizeY; //for movement restriction
        private Animator youmuAnimator;
        private static readonly int HorizontalDirection = Animator.StringToHash("HorizontalSpeed");
        [SerializeField] private GameObject hitBoxGameObject;
        public int instantSpellFrame = 60;
        private bool inInstantSpellCheck;

        private bool inHitRecovery;

        [SerializeField]
        private VisualEffect hitBurstVFX;

        [SerializeField] private float hitRecoverySpeed=1;
        //Events
        public Action onYoumuHit;
        public Action onInstantSpellCheck;

        private Health healthRef;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Singleton YoumuController may already have an instance @"+
                                 instance.gameObject.name);
            }
            instance = this;
        }

        private void Start()
        {
            healthRef = LogicSystemAPI.instance.Health;

            currentSpeed = normalSpeed;
            youmuAnimator = GetComponent<Animator>();
            //Get the size of the sprite (bounding box)
            var spriteSize = GetComponent<SpriteRenderer>().bounds.size;
            halfSizeX = spriteSize.x / 2;
            halfSizeY = spriteSize.y / 2;
            hitBoxGameObject.SetActive(false);
            inInstantSpellCheck = false;
            inHitRecovery = false;
            hitBurstVFX.enabled = false;
        }

        private void Update()
        {
            if (inHitRecovery) return;
            var targetPosition = transform.position + currentSpeed * Time.deltaTime * moveDirection;
            //Clamp the position so that Youmu stays in the movement field.
            targetPosition.x = Mathf.Clamp(targetPosition.x, FieldBoundaries.instance.left + halfSizeX,
                FieldBoundaries.instance.right - halfSizeX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, FieldBoundaries.instance.down + halfSizeY,
                FieldBoundaries.instance.up - halfSizeY);
            //Update position
            transform.position = targetPosition;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector2>().normalized;
            youmuAnimator.SetFloat(HorizontalDirection,moveDirection.x);
        }
        
        public void OnSlow(InputAction.CallbackContext context)
        {
            if (context.phase is InputActionPhase.Performed)
            {
                currentSpeed = lowSpeed;
                hitBoxGameObject.SetActive(true);
            }
            else
            {
                currentSpeed = normalSpeed;
                hitBoxGameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (healthRef.invincible||inInstantSpellCheck) return;
            if (!col.gameObject.CompareTag("EnemyBullet")) return;
            var bullet = col.gameObject.GetComponent<Bullet>();
            if (bullet == null)
            {
                Debug.LogWarning("Bullet without bullet component detected!");
                return;
            }

            StartCoroutine(InstantSpellDelay());
        }

        private IEnumerator InstantSpellDelay()
        {
            inInstantSpellCheck = true;
            onInstantSpellCheck?.Invoke();
            for (var i = 0; i < instantSpellFrame; i++)
            {
                if (healthRef.invincible)
                {
                    inInstantSpellCheck = false;
                    yield break;
                }
                yield return null;
            }
            
            onYoumuHit?.Invoke();
            inInstantSpellCheck = false;
            
            StartCoroutine(HitRecovery());

        }

        private IEnumerator HitRecovery()
        {
            inHitRecovery = true;
            hitBurstVFX.transform.position = transform.position;
            hitBurstVFX.enabled = true;
            hitBurstVFX.Play();
            transform.position = FieldBoundaries.instance.hitRecoveryStartPos;
            while ((transform.position - FieldBoundaries.instance.hitRecoveryEndPos).magnitude > 0.1f)
            {
                var transform1 = transform;
                var position = transform1.position;
                position += (FieldBoundaries.instance.hitRecoveryEndPos - position).normalized *
                            hitRecoverySpeed * Time.deltaTime;
                transform1.position = position;
                yield return null;
            }

            inHitRecovery = false;
             hitBurstVFX.Reinit();
             hitBurstVFX.enabled = false;
           

        }
        
    }
}