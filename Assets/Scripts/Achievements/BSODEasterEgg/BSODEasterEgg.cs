using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;

public class BSODEasterEgg : MonoBehaviour
{
    [SerializeField] AchievementSystem _as;
    [SerializeField] Cinematic cinematic;
    [SerializeField] Canvas BSODCanvas;
    [SerializeField] AudioSource windowsErrorSound;
    bool launched = false;

    private void Update()
    {
        if (!launched && Input.GetKeyDown(KeyCode.B))
        {
            launched = true;
            cinematic.StopCinematic();
            StartCoroutine(ShowBSOD());
        }
    }

    IEnumerator ShowBSOD()
    {
        BSODCanvas.gameObject.SetActive(true);
        windowsErrorSound.PlayOneShot(windowsErrorSound.clip);

        yield return new WaitForSeconds(5f);

        Cursor.lockState = CursorLockMode.None;
        _as.AddProgress(AchievementNames.EasterEgg3, 1);

        string path = Application.persistentDataPath + "/game_data.json";
        if (File.Exists(path)) File.Delete(path);
        SceneManager.LoadScene("MainMenu");
    }
}
