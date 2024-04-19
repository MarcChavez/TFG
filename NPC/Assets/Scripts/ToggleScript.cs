using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleScript : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    
    private void Awake() {
        toggleReference.action.started += Toggle;
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext callbackContext) {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
    }

}
