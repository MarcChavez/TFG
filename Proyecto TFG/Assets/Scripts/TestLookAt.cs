using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestLookAt : MonoBehaviour
{
    private Animator animator;
    public Transform playerToLookAt;
    public Transform Head;
    public Transform LeftHand;
    public Transform RigthHand;
    private bool lookingAtCharacter = false;
    public GameObject phone;


    private void Update() {
        Vector3 direccion = playerToLookAt.position - transform.position;
        //direccion.y = 0; // Elimina la componente Y
        
        Head.LookAt(direccion);
        // Rota para mirar hacia el objetivo
        if (direccion != Vector3.zero)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Euler(0, rotacionDeseada.eulerAngles.y, 0);
            
        }
    }

}
