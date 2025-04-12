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
        // Проверяем, является ли объект NavMeshAgent
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            // Увеличиваем скорость агента
            agent.speed *= speedMultiplier;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, является ли объект NavMeshAgent
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            // Возвращаем исходную скорость агента
            agent.speed /= speedMultiplier;
        }
    }
}
