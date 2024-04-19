using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followAI : MonoBehaviour
{
    private NavMeshAgent ai;
    public Transform player;
    public Animator aiAnim;
    Vector3 dest;

    bool siguiendo = true;

    private void Awake() {
        FindObjectOfType<PlayerInteract>().onEnterRange.AddListener(dejarDeSeguir);
        FindObjectOfType<PlayerInteract>().onExitRange.AddListener(vuelveASeguir);
    }
    
    private void Start() {
        ai = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (siguiendo) {
            dest = player.position;
            ai.destination = dest;
            aiAnim.SetTrigger("Walk");
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                aiAnim.ResetTrigger("Walk");
                aiAnim.SetTrigger("Idle");
            }
            else
            {
                aiAnim.ResetTrigger("Idle");
                aiAnim.SetTrigger("Walk");
            }
        }
        
    }

    private void dejarDeSeguir() {
        siguiendo = false;
    }

    private void vuelveASeguir() {
        siguiendo = true;
    }

}
