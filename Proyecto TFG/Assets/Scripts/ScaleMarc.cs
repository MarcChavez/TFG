using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class ScaleMarc : MonoBehaviour
{
        public RootMotion.FinalIK.VRIK ik;
        public float scaleMlp = 1f;
        public GameObject avatar;
        public GameObject currentAvatar;

        public ControllerBinding ScalePlayerInput = ControllerBinding.AButtonDown;

        void LateUpdate() {
            if (InputBridge.Instance.GetControllerBindingValue(ScalePlayerInput)) {
                GameObject instance = Instantiate(avatar, currentAvatar.transform.position, currentAvatar.transform.rotation);
                // Destruir el objeto instanciado después de destroyAfterSeconds segundos
                Destroy(instance);
            }
        }
}
