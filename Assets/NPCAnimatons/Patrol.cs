using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    [Tooltip("Dolaþýlacak noktalar")] public Transform[] points;

    private NavMeshAgent agent;
    private int idx = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (points == null || points.Length == 0)
        {
            Debug.LogWarning("Patrol: points dizisi boþ!");
            enabled = false;
            return;
        }
        agent.SetDestination(points[idx].position);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.2f)
        {
            idx = (idx + 1) % points.Length;
            agent.SetDestination(points[idx].position);
        }
    }
}