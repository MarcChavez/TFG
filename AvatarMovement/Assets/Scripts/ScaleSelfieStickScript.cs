using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScaleSelfieStickScript : MonoBehaviour
{
    public InputActionReference joystickActionReference = null;
    private MeshRenderer meshRenderer = null;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update() {
        float value = joystickActionReference.action.ReadValue<Vector2>().y;
        OnJoystickMoved(value);
    }

    private void OnJoystickMoved(float yMovement)
    {
        float colorValue = Remap(yMovement, -1, 1, 0, 1);
        meshRenderer.material.color = new Color(colorValue,colorValue,colorValue);
    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
