using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{
    private Animator animator;
    public Transform playerToLookAt;
    private bool lookingAtCharacter = false;
    public GameObject phone;
    public GameObject UIObject;
    private WanderingNPC wanderingScript;
    private AimAtCharacter aimAtCharacter;
    public CameraManager cameraManager;

    private void Awake() {
        animator = GetComponent<Animator>();
        wanderingScript = GetComponentInParent<WanderingNPC>();
        aimAtCharacter = GetComponentInParent<AimAtCharacter>();
        phone.SetActive(false);
        UIObject.SetActive(false);
    }

    private void Update() {
        if (lookingAtCharacter) {
            Vector3 direccion = playerToLookAt.position - transform.position;
            direccion.y = 0; // Elimina la componente Y

            // Rota para mirar hacia el objetivo
            if (direccion != Vector3.zero)
            {
                Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
                transform.rotation = Quaternion.Euler(0, rotacionDeseada.eulerAngles.y, 0);
            }
        }
    }

    public void showUI(bool show) {
        UIObject.SetActive(show);
    }

    public void TakePhoto(){
        animator.SetTrigger("TakeCamera");
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("LookAtPlayer");
        phone.SetActive(true);
        lookingAtCharacter = true;
        wanderingScript.MoveController(false);
    }

    public void ImCloseToPlayer() {
        aimAtCharacter.ActivarMultiAimConstraints();
        cameraManager.allowNpcCamera();
    }

    public void ImNotCloseToPlayer() {
        aimAtCharacter.DesactivarMultiAimConstraints();
        cameraManager.quitNpcCamera();
    }

    public void lookAtCharacter(Transform player) {
        lookingAtCharacter = true;
        wanderingScript.MoveController(false);
        animator.SetTrigger("LookAtPlayer");
        animator.ResetTrigger("TakeCamera");
        animator.ResetTrigger("Walk");
        phone.SetActive(false);
    }

    public void StopInteractWithPlayer(){
        lookingAtCharacter = false;
        animator.SetTrigger("Walk");
        animator.ResetTrigger("TakeCamera");
        animator.ResetTrigger("LookAtPlayer");
        wanderingScript.MoveController(true);
        phone.SetActive(false);
    }

}
