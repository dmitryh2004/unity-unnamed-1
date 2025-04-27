using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;

public enum AchievementNames
{
    FirstStep = 0,
    Amateur = 1,
    Amateur2 = 2,
    Speedrunner1 = 3, //начинающий
    Speedrunner2 = 4, //опытный
    Speedrunner3 = 5, //ветеран
    SpeedrunMaster1 = 6, //профи
    SpeedrunMaster2 = 7, //мастер
    SpeedrunMaster3 = 8, //эксперт
    SpeedrunMaster4 = 9, //Дрим
    Doomguy = 10, //Думгай
    Pacifist = 11,
    Hardcorer = 12,
    EasterEgg1 = 13,
    EasterEgg2 = 14,
    EasterEgg3 = 15
}

public class AchievementSystem : MonoBehaviour
{
    const string achievementFileLocation = "/achievements.json";
    [SerializeField] private List<Achievement> achievementTemplates;
    private List<Achievement> achievements = new List<Achievement>();
    [SerializeField] private List<AchievementController> achievementControllers;
    private List<List<AchievementNames>> achievementQueues = new List<List<AchievementNames>>();

    private void Start()
    {
        for (int i = 0; i < achievementControllers.Count; i++)
        {
            achievementQueues.Add(new List<AchievementNames>());
        }
        LoadAchievements();
        StartCoroutine(CheckQueueCoroutine());
    }

    public Achievement FindAchievementById(AchievementNames id)
    {
        foreach (Achievement a in achievements)
        {
            if (id == a.id)
            {
                return a;
            }
        }
        return null;
    }

    void LoadAchievements()
    {
        string filePath = Application.persistentDataPath + achievementFileLocation;

        foreach (Achievement a in achievementTemplates)
        {
            achievements.Add(a);
        }

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            AchievementList al = JsonUtility.FromJson<AchievementList>(jsonData);

            foreach(AchievementProgress ap in al.achievements)
            {
                Achievement a = FindAchievementById(ap.id);
                a.currentValue = ap.currentValue;
                a.achieved = ap.achieved;
            }
        }
        else
        {
            SaveAchievements();
            Debug.LogWarning("Файл достижений не найден. Создан пустой файл достижений");
        }
    }

    void SaveAchievements()
    {
        AchievementList al = new AchievementList();
        foreach (Achievement a in achievements)
        {
            AchievementProgress ap = new AchievementProgress(a);
            al.achievements.Add(ap);
        }

        string jsonData = JsonUtility.ToJson(al);

        string filePath = Application.persistentDataPath + achievementFileLocation;
        File.WriteAllText(filePath, jsonData);
        Debug.Log("Файл достижений сохранен");
    }

    public void AddProgress(AchievementNames achId, int progress)
    {
        Achievement a = FindAchievementById(achId);
        if (!a.achieved)
        {
            a.currentValue += progress;
            if (a.currentValue >= a.targetValue)
            {
                a.achieved = true;
                LaunchAchieve(achId);
            }
            SaveAchievements();
        }
    }

    public void LaunchAchieve(AchievementNames achId)
    {
        int minQueueIndex = 0;
        int minQueue = 100;
        for(int queueIndex = 0; queueIndex < achievementQueues.Count; queueIndex++)
        {
            List<AchievementNames> queue = achievementQueues[queueIndex];
            if (queue.Count < minQueue)
            {
                minQueue = queue.Count; minQueueIndex = queueIndex;
            }
        }

        achievementQueues[minQueueIndex].Add(achId);
    }

    IEnumerator CheckQueueCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < achievementQueues.Count; i++)
        {
            List<AchievementNames> queue = achievementQueues[i];

            Debug.Log("Queue " + i + " - count of achs: " + queue.Count);

            if (queue.Count == 0) continue;

            if (achievementControllers[i].IsCurrentlyUsed()) continue;

            AchievementNames ach = queue[0];
            queue.RemoveAt(0);
            Debug.Log("Show achievement " + ach + " on controller " + i);
            achievementControllers[i].ShowAchievement(FindAchievementById(ach));

            
        }

        StartCoroutine(CheckQueueCoroutine());
    }
}
