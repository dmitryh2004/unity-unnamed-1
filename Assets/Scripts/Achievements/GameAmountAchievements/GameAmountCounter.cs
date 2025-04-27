using UnityEngine;

public class GameAmountCounter : MonoBehaviour
{
    [SerializeField] AchievementSystem _as;
    public void CountGame()
    {
        _as.AddProgress(AchievementNames.FirstStep, 1);
        _as.AddProgress(AchievementNames.Amateur, 1);
        _as.AddProgress(AchievementNames.Amateur2, 1);
    }

    public void CountPassedGame()
    {
        _as.AddProgress(AchievementNames.Speedrunner1, 1);
        _as.AddProgress(AchievementNames.Speedrunner2, 1);
        _as.AddProgress(AchievementNames.Speedrunner3, 1);
    }
}
