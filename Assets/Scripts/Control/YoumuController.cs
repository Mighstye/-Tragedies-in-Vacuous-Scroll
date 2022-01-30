using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
    public class YoumuController : MonoBehaviour
    {
        public YoumuController instance { get; private set; }
        //Move related fields
        private Vector3 moveDirection;
        private float currentSpeed;
        [SerializeField] private float normalSpeed;
        [SerializeField] private float lowSpeed;
        //Sprite and Animation related fields
        private float halfSizeX, halfSizeY; //for movement restriction
        private Animator youmuAnimator;
        private static readonly int HorizontalDirection = Animator.StringToHash("HorizontalDirection");

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
            currentSpeed = normalSpeed;
            youmuAnimator = GetComponent<Animator>();
            //Get the size of the sprite (bounding box)
            var spriteSize = GetComponent<SpriteRenderer>().bounds.size;
            halfSizeX = spriteSize.x / 2;
            halfSizeY = spriteSize.y / 2;
        }

        private void Update()
        {
            var targetPosition = transform.position + currentSpeed * Time.deltaTime * moveDirection;
            //Clamp the position so that Youmu stays in the movement field.
            targetPosition.x = Mathf.Clamp(targetPosition.x, FieldBoundaries.instance.left + halfSizeX,
                FieldBoundaries.instance.right - halfSizeY);
            targetPosition.y = Mathf.Clamp(targetPosition.y, FieldBoundaries.instance.down + halfSizeY,
                FieldBoundaries.instance.up - halfSizeY);
            //Update position
            transform.position = targetPosition;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector2>().normalized;
            //Todo: decom: youmuAnimator.SetFloat(HorizontalDirection,moveDirection.x);
        }

        public void OnSlow(InputAction.CallbackContext context)
        {
            if (context.phase is InputActionPhase.Performed)
            {
                currentSpeed = lowSpeed;
                //todo: show hit box
            }
            else
            {
                currentSpeed = normalSpeed;
                //todo: hide hit box
            }
        }
    }
}