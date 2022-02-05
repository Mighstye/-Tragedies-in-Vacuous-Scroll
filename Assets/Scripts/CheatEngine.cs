using System.Collections;
using System.Collections.Generic;
using CardSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatEngine : MonoBehaviour
{
    [SerializeField] private ActiveCard sample;
    public void OnCheat1(InputAction.CallbackContext context)
    {
        if (context.phase is not InputActionPhase.Performed) return;
        var cheatCard = Instantiate(sample, this.gameObject.transform);
        CardSystemManager.instance.AddActiveCard(cheatCard);
    }

    public void OnCheat2(InputAction.CallbackContext context)
    {
        
    }
}
