using System.Collections;
using UnityEngine;

public class VoidDamage : MonoBehaviour
{
    public Transform player;
    PlayerHealth playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        StartCoroutine(VoidDamageRoutine());
    }

    private IEnumerator VoidDamageRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        CheckForPlayerIsInVoid();
        StartCoroutine(VoidDamageRoutine());
    }

    void CheckForPlayerIsInVoid()
    {
        if ((player.position.y < -10) && (playerHealth.GetCurrentHealth() > 0))
        {
            playerHealth.TakeDamage(20, null);
        }
    }
}
