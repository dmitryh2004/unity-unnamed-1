using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SlowdownModifier : MonoBehaviour
{
    NavMeshModifierVolume volume;
    [SerializeField] float speedMultiplier = 0.33f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume = GetComponent<NavMeshModifierVolume>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ������ NavMeshAgent
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            // ����������� �������� ������
            agent.speed *= speedMultiplier;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���������, �������� �� ������ NavMeshAgent
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            // ���������� �������� �������� ������
            agent.speed /= speedMultiplier;
        }
    }
}
