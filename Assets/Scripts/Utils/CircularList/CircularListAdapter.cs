using System;
using AirFishLab.ScrollingList;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils.CircularList
{
    [Obsolete("FAILED EXPERIMENT, DO NOT USE THIS.", true)]
    [RequireComponent(typeof(CircularScrollingList))]
    public class CircularListAdapter : MonoBehaviour
    {
        private CircularScrollingList scrollingList;

        private void OnEnable()
        {
            scrollingList ??= GetComponent<CircularScrollingList>();
        }

        public void MoveOneUnitUp(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed) return;
            scrollingList.MoveOneUnitUp();
        }

        public void MoveOneUnitDown(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed) return;
            scrollingList.MoveOneUnitDown();
        }

        public void MoveItem(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed) return;
            var direction = context.ReadValue<Vector2>();
            switch (direction.y)
            {
                case > 0:
                    scrollingList.MoveOneUnitDown();
                    break;
                case < 0:
                    scrollingList.MoveOneUnitUp();
                    break;
            }
        }

        public void AddItem(GameObject o)
        {
            o.transform.SetParent(transform);
            scrollingList.Refresh();
        }
    }
}