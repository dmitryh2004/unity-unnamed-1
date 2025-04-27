using UnityEngine;
using System.IO;
using TMPro;
using System.Collections;

public class AchievementButton : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] AchievementSystem _as;
    [SerializeField] MainMenuController controller;
    public void onClick()
    {
        _as.ClearAchievements();
        controller.ForceUpdateAchievements();
        text.gameObject.SetActive(true);
        StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(3f);
        text.gameObject.SetActive(false);
    }
}
