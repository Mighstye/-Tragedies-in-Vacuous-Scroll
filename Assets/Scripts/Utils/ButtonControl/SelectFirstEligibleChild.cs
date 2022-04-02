using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Utils
{
    public class SelectFirstEligibleChild : MonoBehaviour
    {

        private void OnEnable()
        {
            StartCoroutine(Select());
        }
        private IEnumerator Select()
        {
            var buttons = GetComponentsInChildren<Button>();
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            foreach (var button in buttons) {
                if (button.navigation.mode is Navigation.Mode.None) continue;
                EventSystem.current.SetSelectedGameObject(gameObject);
                yield break;
            }
        }
    }
}