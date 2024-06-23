using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHeight : MonoBehaviour
{
    public CharacterController characterController; // Referencia al CharacterController
    public GameObject objectToMove;
    public bool pelvis;
    public GameObject head;
    
    // Start is called before the first frame update
    void Update()
    {
        if (characterController != null && objectToMove != null)
        {
            // Calcular el punto más bajo del CharacterController
            if (!pelvis){
                float characterBottom = (characterController.transform.position + characterController.center - new Vector3(0, characterController.height / 2, 0)).y;
                float characterHeadZ = (head.transform.position).z;
                float characterHeadX = (head.transform.position).x;
                objectToMove.transform.position = new Vector3(characterHeadX, characterBottom, characterHeadZ);
             }
             else{
                float characterBottom = (characterController.transform.position + characterController.center + new Vector3(0, characterController.height / 2, 0)).y;
                float characterHeadZ = (head.transform.position).z;
                float characterHeadX = (head.transform.position).x;
                objectToMove.transform.position = new Vector3(characterHeadX, characterBottom, characterHeadZ);
             }

            // Mover el objeto al punto más bajo del CharacterController
        }
    }
}
