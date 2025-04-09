using UnityEngine;
using System.Collections.Generic;

public class DoorAudioPlayer : BaseAudioPlayer
{
    [SerializeField] List<WeightedAudioClip> doorSounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeAudioSource();
    }

    public float PlayDoorSound() {
        return PlayRandomAudio(doorSounds);
    }
}
