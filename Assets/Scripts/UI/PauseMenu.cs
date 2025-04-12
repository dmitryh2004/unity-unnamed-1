using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button resumeButton;
    [SerializeField] Button menuButton;
    bool paused = false;

    public bool IsPaused()
    {
        return paused;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resumeButton.onClick.AddListener(ResumeButtonOnClick);
        menuButton.onClick.AddListener(MenuButtonOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            ChangeMenuState();
        }
    }

    void ChangeMenuState()
    {
        if (paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void ResumeButtonOnClick()
    {
        paused = false;
        ChangeMenuState();
    }

    void MenuButtonOnClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
