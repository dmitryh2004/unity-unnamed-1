using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField] PlayerAudioPlayer player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            player.PlayFallDownSound();
        }
    }
}
