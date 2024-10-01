using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavigation : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    private int currentWaypointIndex = -1;

    private bool isWaiting = false;
    private float waitTime = 5f;
    private float waitTimer = 0f;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update() {
        if (isWaiting) {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f) {
                isWaiting = false;
                SetRandomDestination();
            }
        }
        else {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                    isWaiting = true;
                    waitTimer = waitTime;
                }
            }
        }
    }

    void SetRandomDestination() {
        if (waypoints.Length == 0)
            return;

        int randomIndex;
        do {
            randomIndex = Random.Range(0, waypoints.Length);
        } while (randomIndex == currentWaypointIndex && waypoints.Length > 1);

        currentWaypointIndex = randomIndex;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}
