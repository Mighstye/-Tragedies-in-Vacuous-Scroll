using System;
using System.Collections;
using Logic_System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
    public class Rumble : MonoBehaviour
    {
        private Health healthRef;
        private Spell spellRef;

        private bool isInLastStand = false;
        private void Start()
        {
            healthRef = LogicSystemAPI.instance.health;
            spellRef = LogicSystemAPI.instance.spell;

            YoumuController.instance.onInstantSpellCheck += () =>
            {
                StartCoroutine(InstantSpellCheckRumble());
            };
            YoumuController.instance.onYoumuHit += () =>
            {
                StartCoroutine(HitRumble());
            };
        }

        private static IEnumerator InstantSpellCheckRumble()
        {
            Gamepad.current?.SetMotorSpeeds(0.5f,0.5f);
            yield return new WaitForSeconds(0.3f);
            Gamepad.current?.SetMotorSpeeds(0f,0f);
        }

        private static IEnumerator HitRumble()
        {
            Gamepad.current?.SetMotorSpeeds(0.6f,0.8f);
            yield return new WaitForSeconds(0.5f);
            Gamepad.current?.SetMotorSpeeds(0f,0f);
        }

        private IEnumerator LastStandRumble()
        {
            if(Gamepad.current==null) yield break;
            while (healthRef?.currentHealth <= 0 && spellRef?.currentSpellAmount <= 0)
            {
                Gamepad.current.SetMotorSpeeds(0.4f,0.8f);
                yield return new WaitForSeconds(0.2f);
                Gamepad.current.SetMotorSpeeds(0f,0f);
                yield return new WaitForSeconds(0.1f);
                Gamepad.current.SetMotorSpeeds(0.8f,0.4f);
                yield return new WaitForSeconds(0.2f);
                Gamepad.current.SetMotorSpeeds(0f,0f);
                yield return new WaitForSeconds(1.25f);
            }

            isInLastStand = false;
        }
        private void Update()
        {
            if (isInLastStand || healthRef?.currentHealth > 0 || spellRef?.currentSpellAmount > 0) return;
            isInLastStand = true;
            StartCoroutine(LastStandRumble());
        }
    }
}