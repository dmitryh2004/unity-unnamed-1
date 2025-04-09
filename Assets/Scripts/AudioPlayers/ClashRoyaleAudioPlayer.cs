using System.Collections.Generic;
using UnityEngine;

public class ClashRoyaleAudioPlayer : BaseAudioPlayer
{
    [SerializeField] List<WeightedAudioClip> randomSounds;
    [SerializeField] List<WeightedAudioClip> deathSounds;
    void Start()
    {
        InitializeAudioSource();
    }

    public float PlayRandomSound()
    {
        return PlayRandomAudio(randomSounds);
    }

    public float PlayDeathSound()
    {
        return PlayRandomAudio(deathSounds);
    }
}
