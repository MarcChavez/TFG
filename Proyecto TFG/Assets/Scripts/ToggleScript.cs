using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ToggleScript : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    private bool activaFoto = true;
    
    
    private void Awake() {
        toggleReference.action.started += Toggle;
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        toggleReference.action.started -= Toggle;
    }

    public void ActivaFotografia(bool activa){
        activaFoto = activa;
    }

    

    private void Toggle(InputAction.CallbackContext callbackContext) {
        Debug.Log("AAA");
        if (activaFoto)
        {
            bool isActive = !gameObject.activeSelf;
            gameObject.SetActive(isActive);
        }
    }

}
