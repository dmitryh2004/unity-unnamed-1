using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public GameObject targetHint;
    public Transform terrain;
    private void OnCollisionEnter(Collision collision)
    {
        if (targetHint != null) Destroy(targetHint);
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
                npcHealth.TakeDamage(Random.Range(50, 110), gameObject);
            }
            else if (collision.collider.TryGetComponent<PlayerHealth>(out playerHealth))
            {
                if (playerHealth.GetCurrentHealth() > 0)
                {
                    playerHealth.TakeDamage(Random.Range(50, 110), gameObject);
                }
            }
        }
    }
}
