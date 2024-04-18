using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInteract : MonoBehaviour
{

    // Update is called once per frame
    public UnityEvent onEnterRange;
    public UnityEvent onExitRange;
    public Collider collider1 = null;


    void Update()
    {
        float interactRange = 1f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray) {
            if (collider.TryGetComponent(out NpcInteract npcInteract)) {
                NPCLogic(npcInteract, collider);
            }      
        }
    }

    public NpcInteract GetInteractObject() {
        List<NpcInteract> interactList = new List<NpcInteract>();
        float interactRange = 1f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray) {
                if (collider.TryGetComponent(out NpcInteract npcInteract)) {
                    interactList.Add(npcInteract);
                }
            }

            NpcInteract closestNPC = null;
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

            return closestNPC;
    }

    private void NPCLogic(NpcInteract npcInteract, Collider collider){
            if (Input.GetKeyDown(KeyCode.E)){
                onEnterRange.Invoke();
                npcInteract.interactWithPlayer(transform);
            }   
    }


}
