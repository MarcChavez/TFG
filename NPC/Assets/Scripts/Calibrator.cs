using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Calibrator : MonoBehaviour
{
    public Transform LeftShoulder, LeftHand;
    public Transform RightShoulder, RightHand;
    public Transform Head;
    public Transform HeadControllerVR, LeftControllerVR, rightControllerVR;
    public Transform baseFeet;
    public Transform upperForeArmLeft, lowerForeArmLeft;
    public Transform upperForeArmRight, lowerForeArmRight;
    private float scaleHeight, scaleArms;

    public InputActionReference calibratePressed = null;

    private void Awake() {
        calibratePressed.action.started += calibrateMeasures;
    }

    private void OnDestroy() {
        calibratePressed.action.started -= calibrateMeasures;
    }

    private void calibrateMeasures(InputAction.CallbackContext callbackContext){
        scaleHeight = (HeadControllerVR.position.y - baseFeet.position.y) / (Head.position.y - baseFeet.position.y);
        scaleArms = UnityEngine.Vector3.Distance(LeftShoulder.position, LeftControllerVR.position) / UnityEngine.Vector3.Distance(LeftShoulder.position, LeftHand.position);
        transform.localScale = new UnityEngine.Vector3(scaleHeight, scaleHeight, scaleHeight);
        upperForeArmLeft.localScale = lowerForeArmLeft.localScale = upperForeArmRight.localScale = lowerForeArmRight.localScale 
        = new UnityEngine.Vector3(scaleArms, scaleArms, scaleArms);
    }
    
}
