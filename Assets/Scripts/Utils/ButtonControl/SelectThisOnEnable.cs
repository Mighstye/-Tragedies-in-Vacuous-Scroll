using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Utils
{
    [RequireComponent(typeof(Button))]
    public class SelectThisOnEnable : MonoBehaviour
    {
        private Button button;
        private void OnEnable()
        {
            button = GetComponent<Button>();
            if (button.navigation.mode is Navigation.Mode.None) return;
            StartCoroutine(Select());
        }
        private IEnumerator Select()
        {
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        
        
    }
}