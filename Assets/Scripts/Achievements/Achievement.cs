using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class AchievementProgress
{
    public AchievementNames id;
    public int currentValue;
    public bool achieved;

    public AchievementProgress() { }

    public AchievementProgress(Achievement a)
    {
        this.id = a.id;
        this.currentValue = a.currentValue;
        this.achieved = a.achieved;
    }
}

[System.Serializable]
public class Achievement
{
    public AchievementNames id;
    public string title;
    public string desc;
    public Sprite icon;
    public int targetValue;
    public int currentValue;
    public bool achieved;
}

[System.Serializable] public class AchievementList
{
    public List<AchievementProgress> achievements;

    public AchievementList()
    {
        achievements = new List<AchievementProgress>();
    }
}
