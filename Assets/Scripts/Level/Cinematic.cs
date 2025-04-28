using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour
{
    [SerializeField] AchievementSystem _as;
    [SerializeField] KillCounter killCounter;
    [SerializeField] GameAmountCounter gac;
    [SerializeField] Camera mainCamera, cinematicCamera;
    [SerializeField] Transform player;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] SpaceshipController spaceship;
    [SerializeField] AudioSource bgMusicSource;
    [SerializeField] AudioClip outroMusicClip;
    [SerializeField] GameObject blackScreenUI;
    [SerializeField] GameObject victoryCanvas;
    [SerializeField] Button menuButton;
    [SerializeField] Timer timer;
    [SerializeField] TMP_Text timerText;

    Animator blackScreenUIAnimator;

    Coroutine cinematicCoroutine;

    bool endOfCinematic = false;

    public void StopCinematic()
    {
        bgMusicSource.Stop();
        StopCoroutine(cinematicCoroutine);
    }

    public bool IsEndOfCinematic()
    {
        return endOfCinematic;
    }

    private void Start()
    {
        blackScreenUIAnimator = blackScreenUI.GetComponent<Animator>();
    }

    public void StartCinematic()
    {
        timer.StopTimer(false);
        mainCamera.gameObject.SetActive(false);
        cinematicCamera.gameObject.SetActive(true);

        player.gameObject.SetActive(false);
        inventoryCanvas.SetActive(false);

        bgMusicSource.Stop();
        bgMusicSource.clip = outroMusicClip;
        bgMusicSource.Play();

        cinematicCoroutine = StartCoroutine(PlayCinematic());
    }

    IEnumerator PlayCinematic()
    {
        cinematicCamera.GetComponent<Animator>().SetTrigger("StartCameraMovement");

        yield return new WaitForSeconds(2f);
        spaceship.CloseDoor();
        yield return new WaitForSeconds(3f);
        spaceship.StartEngine(0);
        spaceship.StartEngine(1);

        yield return new WaitForSeconds(1f);
        spaceship.LiftOff();

        yield return new WaitForSeconds(12f);
        blackScreenUIAnimator.SetTrigger("Activate");
        
        yield return new WaitForSeconds(3f);

        endOfCinematic = true;
        gac.CountGame();
        gac.CountPassedGame();

        if (killCounter.GetKilledByPlayer() == 0) _as.AddProgress(AchievementNames.Pacifist, 1);
        if (killCounter.AreAllKilled()) _as.AddProgress(AchievementNames.Doomguy, 1);

        string path = Application.persistentDataPath + "/game_data.json";
        if (!File.Exists(path)) _as.AddProgress(AchievementNames.Hardcorer, 1);

        float elapsedTime = timer.GetElapsedTime();
        if (elapsedTime <= 180f) _as.AddProgress(AchievementNames.SpeedrunMaster1, 1);
        if (elapsedTime <= 165f) _as.AddProgress(AchievementNames.SpeedrunMaster2, 1);
        if (elapsedTime <= 150f) _as.AddProgress(AchievementNames.SpeedrunMaster3, 1);
        if (elapsedTime <= 140f) _as.AddProgress(AchievementNames.SpeedrunMaster4, 1);

        victoryCanvas.SetActive(true);
        timerText.SetText("Время забега: " + timer.GetText());
        Cursor.lockState = CursorLockMode.None;

        menuButton.onClick.AddListener(MainMenuButtonOnClick);
    }

    void MainMenuButtonOnClick()
    {
        string path = Application.persistentDataPath + "/game_data.json";
        if (File.Exists(path)) File.Delete(path);
        SceneManager.LoadScene("MainMenu");
    }
}
