using System.Collections;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    [SerializeField] int health = 1;
    public int getHP()
    {
        return health;
    }

    public void setHP(int hp)
    {
        health = hp;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            AmogusController amogusController;
            if (TryGetComponent<AmogusController>(out amogusController))
            {
                amogusController.StopAllCoroutines();
            }
            ClashRoyaleController clashRoyaleController;
            if (TryGetComponent<ClashRoyaleController>(out clashRoyaleController))
            {
                clashRoyaleController.StopAllCoroutines();
            }

            float deathAudioLength = 0f;
            BaseAudioPlayer audioPlayer;
            if (TryGetComponent<BaseAudioPlayer>(out audioPlayer))
            {
                if (audioPlayer is AmogusAudioPlayer amogusAudioPlayer)
                {
                    deathAudioLength = amogusAudioPlayer.PlayDeathSound();
                }
                else if (audioPlayer is ClashRoyaleAudioPlayer clashRoyaleAudioPlayer)
                {
                    deathAudioLength = clashRoyaleAudioPlayer.PlayDeathSound();
                }
            }
            if (transform.childCount > 0)
                StartCoroutine(DelayedDestroy(deathAudioLength));
            else
                gameObject.SetActive(false);
        }
    }

    IEnumerator DelayedDestroy(float delay)
    {
        transform.GetChild(0).gameObject.SetActive(false); //скрываем модель
        GetComponent<Collider>().enabled = false; //отключаем коллайдер
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
