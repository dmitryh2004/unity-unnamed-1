using UnityEngine;

public class MeteoriteSpawnStart : MonoBehaviour
{
    [SerializeField] MeteoriteSpawner meteoriteSpawner;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            meteoriteSpawner.StartSpawn();
            Destroy(this);
        }
    }
}
