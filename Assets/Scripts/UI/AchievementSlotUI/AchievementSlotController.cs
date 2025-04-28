using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementSlotController : MonoBehaviour
{
    [SerializeField] AchievementSystem _as;
    [SerializeField] AchievementNames achId;

    [SerializeField] Sprite unknown;
    [SerializeField] Image AchievementImage;
    [SerializeField] TMP_Text AchievementTitle;
    [SerializeField] TMP_Text AchievementDesc;
    [SerializeField] TMP_Text AchievementProgress;
    [SerializeField] Image AchievementProgressBar;

    Color inProgress = new Color(0f, .95f, .95f);
    Color completed = new Color(0f, .95f, 0f);

    [SerializeField] float maxWidth, height;

    public void UpdateAchievement()
    {
        Achievement ach = _as.FindAchievementById(achId);

        if (ach.achieved)
            AchievementImage.sprite = ach.icon;
        else
            AchievementImage.sprite = unknown;
        AchievementTitle.text = ach.title;
        AchievementDesc.text = ach.desc;

        AchievementProgress.text = $"{ach.currentValue} / {ach.targetValue}";

        float progressValue = Mathf.Clamp01((float)ach.currentValue / ach.targetValue);
        float width = progressValue * maxWidth;
        Debug.Log($"{achId}, {width}");

        AchievementProgressBar.rectTransform.sizeDelta = new Vector2(width, height);
        if (progressValue == 1.0f)
        {
            AchievementProgressBar.color = completed;
        }
        else
        {
            AchievementProgressBar.color = inProgress;
        }
    }
}
