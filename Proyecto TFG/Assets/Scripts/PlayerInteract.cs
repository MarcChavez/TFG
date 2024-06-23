using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerInteract : MonoBehaviour
{

    // Update is called once per frame
    public bool intercated = false;
    private bool characterClose = false;
    public NpcInteract closestNPC = null;
    public InputActionReference interactWithNPC = null;
    
    private void Awake() {
        interactWithNPC.action.started += NPcInteract;
    }

    private void OnDestroy() {
        interactWithNPC.action.started -= NPcInteract;
    }

    private void NPcInteract(InputAction.CallbackContext callbackContext) {
        intercated = !intercated;
    }


    void Update()
    {
        if (closestNPC != null && Vector3.Distance(transform.position, closestNPC.transform.position) > 10){
            closestNPC.StopInteractWithPlayer(); 
                closestNPC.showUI(false);
                closestNPC.ImNotCloseToPlayer();
                closestNPC = null;
        }
        CalculaClosest();
        if (closestNPC != null && intercated){
                
                closestNPC.TakePhoto();
            }
            else if (closestNPC != null && !intercated){
                closestNPC.lookAtCharacter(transform);
            } 
        
    }

    private void CalculaClosest(){
        List<NpcInteract> interactList = new List<NpcInteract>();
        float interactRange = 3.5f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray) {
            if (collider.TryGetComponent(out NpcInteract npcInteract)) {
                interactList.Add(npcInteract);
            }      
        }

        if (interactList.Count == 0)
        {
            if (closestNPC != null && !intercated)
            {
                closestNPC.StopInteractWithPlayer(); 
                closestNPC.showUI(false);
                closestNPC.ImNotCloseToPlayer();
                closestNPC = null;
            }
        }

        else if (closestNPC == null) {
            foreach (NpcInteract npcInteract in interactList) {
                if (closestNPC == null) {
                    closestNPC = npcInteract;
                }
                else {
                    if (Vector3.Distance(transform.position, npcInteract.transform.position) < Vector3.Distance(transform.position, closestNPC.transform.position)) {
                        closestNPC = npcInteract;
                    }
                }
            }

            NPCLogic(closestNPC);
            
        }
    }

    private void NPCLogic(NpcInteract npcInteract){
            npcInteract.ImCloseToPlayer();
            npcInteract.stopAllGroup(transform);
            npcInteract.showUI(true);  
    }


}
