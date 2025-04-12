using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolController : MonoBehaviour
{
    [SerializeField] float changeWaypointDelay = 0.25f; //задержка выбора следующей точки
    [SerializeField] List<Transform> waypoints;
    [SerializeField] int startFrom = 0;
    NavMeshAgent agent;
    bool isMoving = false;
    int currentWaypoint = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWaypoint = startFrom;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[currentWaypoint].position);
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && (agent.remainingDistance < 0.5f))
        {
            isMoving = false;
            currentWaypoint++;
            currentWaypoint %= waypoints.Count;
            StartCoroutine(PatrolDelayCoroutine());
        }
    }

    private IEnumerator PatrolDelayCoroutine()
    {
        yield return new WaitForSeconds(changeWaypointDelay);
        agent.SetDestination(waypoints[currentWaypoint].position);
        isMoving = true;
    }
}
