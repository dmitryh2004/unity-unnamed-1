using UnityEngine;

public class RedSphereController : MonoBehaviour
{
    [SerializeField] Transform player;
    PlayerHealth playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == player.gameObject)
        {
            playerHealth.TakeDamage(50, gameObject);
            GetComponent<NPCHealth>().TakeDamage(100);
        }
    }
}
