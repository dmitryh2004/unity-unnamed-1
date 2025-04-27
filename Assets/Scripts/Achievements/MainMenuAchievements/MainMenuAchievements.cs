using UnityEngine;

public class MainMenuAchievements : MonoBehaviour
{
    [SerializeField] AchievementSystem _as;
    [SerializeField] Timer timer;
    int achievementsButtonPressed = 0;

    private void Start()
    {
        timer.StartTimer();
    }
    public void AddAchButtonPressed()
    {
        if (!_as.FindAchievementById(AchievementNames.EasterEgg1).achieved)
        {
            achievementsButtonPressed++;
            if (achievementsButtonPressed == 10)
            {
                _as.AddProgress(AchievementNames.EasterEgg1, 1);
            }
        }
    }

    private void Update()
    {
        if (!_as.FindAchievementById(AchievementNames.EasterEgg2).achieved && (timer.GetElapsedTime() > 300f))
        {
            _as.AddProgress(AchievementNames.EasterEgg2, 1);
        }
    }
}
