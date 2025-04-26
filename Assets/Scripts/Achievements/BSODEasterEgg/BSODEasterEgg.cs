using UnityEngine;
using Unity.IO;
using UnityEngine.SceneManagement;
using System.Collections;

public class BSODEasterEgg : MonoBehaviour
{
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

        yield return new WaitForSeconds(2f);

        Cursor.lockState = CursorLockMode.None;

        //string path = Application.persistentDataPath + "/game_data.json";
        //if (File.Exists(path)) File.Delete(path);
        SceneManager.LoadScene("MainMenu");
    }
}
