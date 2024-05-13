using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class WalkPatrol : MonoBehaviour
{
 
    public Animator aiAnim; 
    public Transform[] points;
    public bool WpReached;
     
    private void Start() {
        int destPoint = Random.Range(0, points.Length);
        foreach (Transform child in transform)
        {
            NavMeshAgent agent = child.GetComponent<NavMeshAgent>();
            if (agent != null) // Comprueba si el hijo tiene un componente NavMeshAgent
            {
                GotoNextPoint(agent, destPoint);
            }
        }
    }

    void GotoNextPoint(NavMeshAgent agent, int destPoint)
    {
        // Devuelve si no hay puntos
        WpReached = false;
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
    }
 
    // Update is called once per frame
    void Update()
    {
        int destPoint = Random.Range(0, points.Length);
        foreach (Transform child in transform)
        {
            NavMeshAgent agent = child.GetComponent<NavMeshAgent>();
            if (agent != null && !agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint(agent, destPoint);
            }
        }
    }
 
}