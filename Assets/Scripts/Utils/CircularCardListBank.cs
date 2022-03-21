using System;
using System.Collections.Generic;
using AirFishLab.ScrollingList;
using CardSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils
{
    public class CircularCardListBank : BaseListBank
    {
        [SerializeField]private List<Card> cards;
        [SerializeField]private CircularScrollingList list;

        private void Start()
        {
            list ??= GetComponent<CircularScrollingList>();
            cards = new List<Card>(GetComponentsInChildren<Card>(true));
        }

        public void UpdateContent(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed) return;
            cards = new List<Card>(GetComponentsInChildren<Card>());
            list.Refresh(0);
        }

        public override object GetListContent(int index)
        {
            return cards[index];
        }

        public override int GetListLength()
        {
            return cards.Count;
        }
        
        private void OnEnable()
        {
            list ??= GetComponent<CircularScrollingList>();
        }

        public void MoveOneUnitUp(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed) return;
            list.MoveOneUnitUp();
        }

        public void MoveOneUnitDown(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed) return;
            list.MoveOneUnitDown();
        }

        public void MoveItem(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed) return;
            var direction = context.ReadValue<Vector2>();
            switch (direction.y)
            {
                case > 0:
                    list.MoveOneUnitDown();
                    break;
                case < 0:
                    list.MoveOneUnitUp();
                    break;
            }
        }

        public void AddItem(Card c)
        {
            c.transform.SetParent(transform);
            cards.Add(c);
            list.Refresh(-1);
        }
    }
}