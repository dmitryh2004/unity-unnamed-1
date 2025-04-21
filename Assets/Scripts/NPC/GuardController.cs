using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    ChaseController chaseController;
    [SerializeField] List<Transform> possibleTargets;
    [SerializeField] float guardRadius;
    private void Start()
    {
        chaseController = GetComponent<ChaseController>();
    }

    private void Update()
    {
        Transform newTarget = null;
        foreach (Transform possibleTarget in possibleTargets)
        {
            float possibleDistance = Vector3.Distance(transform.position, possibleTarget.transform.position);
            if (newTarget == null)
            {
                if (possibleDistance < guardRadius)
                    newTarget = possibleTarget;
            }
            else
            {
                float currentDistance = Vector3.Distance(transform.position, newTarget.transform.position);
                if (possibleDistance < guardRadius)
                {
                    if (possibleDistance < currentDistance)
                    {
                        newTarget = possibleTarget;
                    }
                }
            }
        }
        chaseController.SetTarget(newTarget);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, guardRadius);
    }
}
