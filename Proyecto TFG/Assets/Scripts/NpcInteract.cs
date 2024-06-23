using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NpcInteract : MonoBehaviour
{
    private Animator animator;
    public Transform playerToLookAt;
    private bool lookingAtCharacter = false;
    public GameObject phone;
    public GameObject UIObject;
    private NavMeshAgent agent;
    private AimAtCharacter aimAtCharacter;
    public CameraManager cameraManager;
    public UnityEvent<Transform> imCloseToPlayer;
    public UnityEvent imFarFromPlayer;

    private void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
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

    private void Start() {
        if (imCloseToPlayer == null)
        {
            imCloseToPlayer = new UnityEvent<Transform>();
        }
        if (imFarFromPlayer == null)
        {
            imFarFromPlayer = new UnityEvent();
        }
    }

    public void setPlayer(CameraManager cameraManagerOut, Transform playerToLookAtOut){
        cameraManager = cameraManagerOut;
        playerToLookAt = playerToLookAtOut;
    }

    public void stopAllGroup(Transform player) {
          imCloseToPlayer.Invoke(player);
    }

    public void moveAllGroup() {
        imFarFromPlayer.Invoke();
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
        agent.isStopped = true;
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
        agent.isStopped = true;
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
        agent.isStopped = false;
        phone.SetActive(false);
    }

}
