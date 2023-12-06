using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatNav : MonoBehaviour
{
    public Transform targetObject;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on this GameObject.");
        }
    }

    void Update()
    {
        if (targetObject != null)
        {
            // Set the destination of the NavMeshAgent to the position of the target object
            navMeshAgent.SetDestination(targetObject.position);
        }
    }
}
