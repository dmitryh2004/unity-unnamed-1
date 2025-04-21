using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainFragment;
    [SerializeField] GameObject creditsFragment;
    [SerializeField] Button newGameButton;
    [SerializeField] Button loadSaveButton;
    [SerializeField] Button creditsButton;
    [SerializeField] Button backButton;
    [SerializeField] Button exitButton;
    void Start()
    {
        Time.timeScale = 1f;

        newGameButton.onClick.AddListener(NewGameButton);
        loadSaveButton.onClick.AddListener(LoadSaveButton);
        creditsButton.onClick.AddListener(CreditsButton);
        backButton.onClick.AddListener(BackButton);
        exitButton.onClick.AddListener(ExitButton);

        mainFragment.SetActive(true);
        creditsFragment.SetActive(false);
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

    void BackButton()
    {
        mainFragment.SetActive(true);
        creditsFragment.SetActive(false);
    }

    void ExitButton()
    {
        StartCoroutine(ExitApplication());
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
