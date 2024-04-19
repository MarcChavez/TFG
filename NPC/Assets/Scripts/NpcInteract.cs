using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{
    private Animator animator;
    public Transform objeto1;
    private bool lookingAtCharacter = false;
    public GameObject phone;

    private void Awake() {
        animator = GetComponent<Animator>();
        phone.SetActive(false);
    }

    private void Update() {
        if (lookingAtCharacter) {
            Vector3 direccion = objeto1.position - transform.position;
            direccion.y = 0; // Elimina la componente Y

            // Rota para mirar hacia el objetivo
            if (direccion != Vector3.zero)
            {
                Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
                transform.rotation = Quaternion.Euler(0, rotacionDeseada.eulerAngles.y, 0);
            }
        }
    }

    public void showPhone() {
        phone.SetActive(true);
    }

    public void interactWithPlayer(Transform player){
        objeto1 = player;
        lookingAtCharacter = true;
        animator.SetTrigger("Nod");
        animator.ResetTrigger("Idle");
    }

    public void StopInteractWithPlayer(){
        objeto1 = null;
        phone.SetActive(false);
        lookingAtCharacter = false;
        animator.SetTrigger("Idle");
        animator.ResetTrigger("Nod");
    }

}
