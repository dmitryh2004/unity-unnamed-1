using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour
{
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

    public void StopCinematic()
    {
        bgMusicSource.Stop();
        StopCoroutine(cinematicCoroutine);
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

        gac.CountGame();

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
