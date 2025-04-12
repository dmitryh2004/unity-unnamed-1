using UnityEngine;

public class MainMenuAudioSource : MonoBehaviour
{
    [SerializeField] AudioClip buttonClickSound;

    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayButtonClickSound()
    {
        source.PlayOneShot(buttonClickSound);
    }
}
