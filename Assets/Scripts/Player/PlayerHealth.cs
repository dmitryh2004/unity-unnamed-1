using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //public AudioSource damageAudioSource;
    //public AudioSource deathAudioSource;
    public Image interfaceBorderImage;
    public int maxHealth = 100; // ������������ ���������� ��������
    private int currentHealth; // ������� ���������� ��������
    public GameObject UICanvas;
    public GameObject gameOverCanvas; // ������ ���������
    public Button restartButton; // ������ �����������
    public Button loadCheckpointButton; // ������ �������� ���������
    PlayerAudioPlayer audioPlayer;

    void Start()
    {
        // ������������� ������� �������� ������ ������������� ��� ������ ����
        currentHealth = maxHealth;
        gameOverCanvas.SetActive(false);
        restartButton.onClick.AddListener(RestartButtonOnClick);
        loadCheckpointButton.onClick.AddListener(LoadLastCheckpointOnClick);
        audioPlayer = GetComponent<PlayerAudioPlayer>();

        // ��������� ������� ����������
        UpdateInterfaceBorder();
    }

    public void TakeDamage(int damage, GameObject dealer, bool playSound = true)
    {
        if (playSound)
            audioPlayer.PlayTakeDamageSound();
        // ��������� ������� �������� �� �������� �����
        currentHealth -= damage;

        // ���������, ���� �������� ������ ��� ����� 0
        if (currentHealth <= 0)
        {
            currentHealth = 0; // ��������, ��� �������� �� ������ � ������������� ��������
            GameOver(dealer); // �������� ����� ������ ������
        }

        // ��������� ������� ����������
        UpdateInterfaceBorder();
    }

    public void Heal(int amount)
    {
        // ����������� �������� ������ �� ��������� ��������
        currentHealth += amount;

        // ��������, ��� �������� �� ��������� ��������
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // ��������� ������� ����������
        UpdateInterfaceBorder();
    }

    void UpdateInterfaceBorder()
    {
        Color color = interfaceBorderImage.color;
        color.a = 1f - (((float) currentHealth) / maxHealth);
        interfaceBorderImage.color = color;
    }

    public int GetCurrentHealth()
    {
        // ����� ��� ��������� �������� �������� (��������, ��� ����������� �� UI)
        return currentHealth;
    }

    public void SetCurrentHealth(int hp)
    {
        currentHealth = hp;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    private void GameOver(GameObject dealer)
    {
        AmogusAudioPlayer amogusAudioPlayer;
        if ((dealer != null) && dealer.TryGetComponent<AmogusAudioPlayer>(out amogusAudioPlayer))
        {
            amogusAudioPlayer.PlayKillSound();
        }
        else
        {
            audioPlayer.PlayDeathSound();
        }
        UICanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0; // ��������� ������� ��� �����
        Cursor.lockState = CursorLockMode.None;
    }

    private void RestartButtonOnClick()
    {
        RestartGame();
    }

    private void LoadLastCheckpointOnClick()
    {
        RestartGame(true);
    }

    private void RestartGame(bool useSave = false)
    {
        Time.timeScale = 1; // �������������� �������
        gameOverCanvas.SetActive(false);
        currentHealth = maxHealth; // �������������� HP

        if (!useSave) // �������� ����������, ���� ����� ������ ������ �������
        {
            string path = Application.persistentDataPath + "/game_data.json";
            if (File.Exists(path)) File.Delete(path);
        }

        // ���������� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
