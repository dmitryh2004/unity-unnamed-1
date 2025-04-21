using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public Transform terrain;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform == terrain)
        {
            Destroy(gameObject);
        }
        else
        {
            PlayerHealth playerHealth;
            NPCHealth npcHealth;
            if (collision.collider.TryGetComponent<NPCHealth>(out npcHealth))
            {
                npcHealth.TakeDamage(Random.Range(50, 110));
            }
            else if (collision.collider.TryGetComponent<PlayerHealth>(out playerHealth))
            {
                playerHealth.TakeDamage(Random.Range(50, 110), gameObject);
            }
        }
    }
}
