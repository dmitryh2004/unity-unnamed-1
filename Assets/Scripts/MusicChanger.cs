using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] List<WeightedAudioClip> audios;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        int totalWeight = 0;
        foreach (WeightedAudioClip clip in audios)
        {
            totalWeight += clip.weight;
        }

        int value = Random.Range(0, totalWeight) + 1;
        int index = 0;
        int temp = 0;

        for (index = 0; index < audios.Count; index++)
        {
            temp += audios[index].weight;
            if (value <= temp) break;
        }

        audioSource.clip = audios[index].clip;
        audioSource.Play();
    }
}
