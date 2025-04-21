using UnityEngine;
using UnityEngine.AI;

public class ChaseController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] Transform target = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTarget())
        {
            agent.SetDestination(target.position);

            agent.isStopped = (agent.remainingDistance < 3.5f);
        }
        else
        {
            if (agent.hasPath) agent.ResetPath();
        }
        anim.SetBool("moving", hasTarget() && !agent.isStopped);
    }

    public Transform GetTarget()
    {
        return target;
    }

    public bool hasTarget()
    {
        return target != null;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void ClearTarget()
    {
        target = null;
    }
}
