using System.Collections;
using UnityEngine;

public class ClashRoyaleController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform player;
    PlayerHealth playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        StartCoroutine(Attack());
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(5f);

        if (Random.Range(0, 100) < 40)
        {
            ClashRoyaleAudioPlayer audioPlayer = GetComponent<ClashRoyaleAudioPlayer>();
            audioPlayer.PlayRandomSound();
        }
        StartCoroutine(PlaySound());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        Vector3 playerPos = player.position, selfPos = transform.position;
        if (Vector3.Distance(playerPos, selfPos) < 3.5f)
        {
            animator.SetTrigger("attackTrigger");
            playerHealth.TakeDamage(15, gameObject);
        }
        StartCoroutine(Attack());
    }
}
