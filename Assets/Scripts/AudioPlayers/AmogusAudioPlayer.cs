using System.Collections.Generic;
using UnityEngine;

public class AmogusAudioPlayer : BaseAudioPlayer
{
    [SerializeField] List<WeightedAudioClip> idleSounds;
    [SerializeField] List<WeightedAudioClip> chasingSounds;
    [SerializeField] List<WeightedAudioClip> deathSounds;
    [SerializeField] List<WeightedAudioClip> killSounds;

    private void Start()
    {
        InitializeAudioSource();
    }
    public float PlayIdleSound()
    {
        return PlayRandomAudio(idleSounds);
    }

    public float PlayChaseSound()
    {
        return PlayRandomAudio(chasingSounds);
    }

    public float PlayDeathSound()
    {
        return PlayRandomAudio(deathSounds);
    }

    public float PlayKillSound()
    {
        return PlayRandomAudio(killSounds);
    }
}
