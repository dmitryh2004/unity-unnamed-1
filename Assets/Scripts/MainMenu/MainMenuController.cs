using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainFragment;
    [SerializeField] GameObject creditsFragment;
    [SerializeField] GameObject achievementsFragment;
    [SerializeField] MainMenuAchievements mainMenuAchievements;
    [SerializeField] List<AchievementSlotController> achievementControllers = new List<AchievementSlotController>();
    [SerializeField] Button newGameButton;
    [SerializeField] Button loadSaveButton;
    [SerializeField] Button creditsButton;
    [SerializeField] Button achievementsButton;
    [SerializeField] Button creditsBackButton;
    [SerializeField] Button achievementsBackButton;
    [SerializeField] Button exitButton;
    void Start()
    {
        Time.timeScale = 1f;

        newGameButton.onClick.AddListener(NewGameButton);
        loadSaveButton.onClick.AddListener(LoadSaveButton);
        creditsButton.onClick.AddListener(CreditsButton);
        creditsBackButton.onClick.AddListener(CreditsBackButton);
        achievementsButton.onClick.AddListener(AchievementsButton);
        achievementsBackButton.onClick.AddListener(AchievementsBackButton);
        exitButton.onClick.AddListener(ExitButton);

        mainFragment.SetActive(true);
        creditsFragment.SetActive(false);
        achievementsFragment.SetActive(false);
    }

    void NewGameButton()
    {
        string path = Application.persistentDataPath + "/game_data.json";
        if (File.Exists(path)) File.Delete(path);
        LoadSaveButton();
    }

    void LoadSaveButton()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(0.35f);
        SceneManager.LoadScene("Level");
    }

    void CreditsButton()
    {
        mainFragment.SetActive(false);
        creditsFragment.SetActive(true);
    }

    void CreditsBackButton()
    {
        mainFragment.SetActive(true);
        creditsFragment.SetActive(false);
    }

    void AchievementsButton()
    {
        mainMenuAchievements.AddAchButtonPressed();
        mainFragment.SetActive(false);
        achievementsFragment.SetActive(true);
        foreach (AchievementSlotController asc in achievementControllers)
        {
            asc.UpdateAchievement();
        }
    }

    void AchievementsBackButton()
    {
        mainFragment.SetActive(true);
        achievementsFragment.SetActive(false);
    }

    void ExitButton()
    {
        StartCoroutine(ExitApplication());
    }

    public void ForceUpdateAchievements()
    {
        foreach (AchievementSlotController asc in achievementControllers)
        {
            asc.UpdateAchievement();
        }
    }

    IEnumerator ExitApplication()
    {
        yield return new WaitForSeconds(0.35f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
