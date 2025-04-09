using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioPlayer : BaseAudioPlayer
{
    [SerializeField] List<WeightedAudioClip> startLevelSounds;
    [SerializeField] List<WeightedAudioClip> interactSounds;
    [SerializeField] List<WeightedAudioClip> takeDamageSounds;
    [SerializeField] List<WeightedAudioClip> fallDownSounds;
    [SerializeField] List<WeightedAudioClip> deathSounds;
    [SerializeField] List<WeightedAudioClip> povezloSounds;
    [SerializeField] List<WeightedAudioClip> runningSounds;
    [SerializeField] List<WeightedAudioClip> screamingSounds;
    [SerializeField] List<WeightedAudioClip> reloadSounds;
    [SerializeField] List<WeightedAudioClip> shootSounds;
    [SerializeField] List<WeightedAudioClip> shootNoAmmoSounds;

    private void Start()
    {
        InitializeAudioSource();
        PlayStartLevelSound();
    }

    public void PlayStartLevelSound()
    {
        PlayRandomAudio(startLevelSounds);
    }

    public void PlayInteractSound()
    {
        PlayRandomAudio(interactSounds);
    }

    public void PlayTakeDamageSound()
    {
        PlayRandomAudio(takeDamageSounds);
    }

    public void PlayFallDownSound()
    {
        PlayRandomAudio(fallDownSounds);
    }

    public void PlayDeathSound()
    {
        PlayRandomAudio(deathSounds);
    }

    public void PlayPovezloSound()
    {
        PlayRandomAudio(povezloSounds);
    }

    public void PlayRunningSound()
    {
        PlayRandomAudio(runningSounds);
    }

    public void PlayScreamingSound()
    {
        PlayRandomAudio(screamingSounds);
    }

    public void PlayShootSound()
    {
        PlayRandomAudio(shootSounds);
    }

    public void PlayShootNoAmmoSound()
    {
        PlayRandomAudio(shootNoAmmoSounds);
    }
    public void PlayReloadSound()
    {
        PlayRandomAudio(reloadSounds);
    }
}
