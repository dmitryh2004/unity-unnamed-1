using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //public AudioSource damageAudioSource;
    //public AudioSource deathAudioSource;
    public Image interfaceBorderImage;
    public int maxHealth = 100; // Максимальное количество здоровья
    private int currentHealth; // Текущее количество здоровья
    public GameObject UICanvas;
    public GameObject gameOverCanvas; // Панель проигрыша
    public Button restartButton; // Кнопка перезапуска
    public Button loadCheckpointButton; // Кнопка загрузки чекпоинта
    public Button mainMenuButton; // Кнопка выхода в главное меню
    PlayerAudioPlayer audioPlayer;

    void Start()
    {
        // Устанавливаем текущее здоровье равным максимальному при старте игры
        currentHealth = maxHealth;
        gameOverCanvas.SetActive(false);
        restartButton.onClick.AddListener(RestartButtonOnClick);
        loadCheckpointButton.onClick.AddListener(LoadLastCheckpointOnClick);
        mainMenuButton.onClick.AddListener(MainMenuOnClick);
        audioPlayer = GetComponent<PlayerAudioPlayer>();

        // Обновляем элемент интерфейса
        UpdateInterfaceBorder();
    }

    public void TakeDamage(int damage, GameObject dealer, bool playSound = true)
    {
        if (playSound)
            audioPlayer.PlayTakeDamageSound();
        // Уменьшаем текущее здоровье на величину урона
        currentHealth -= damage;

        // Проверяем, если здоровье меньше или равно 0
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Убедимся, что здоровье не уходит в отрицательные значения
            GameOver(dealer); // Вызываем метод смерти игрока
        }

        // Обновляем элемент интерфейса
        UpdateInterfaceBorder();
    }

    public void Heal(int amount)
    {
        // Увеличиваем здоровье игрока на указанное значение
        currentHealth += amount;

        // Убедимся, что здоровье не превышает максимум
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Обновляем элемент интерфейса
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
        // Метод для получения текущего здоровья (например, для отображения на UI)
        return currentHealth;
    }

    public void SetCurrentHealth(int hp)
    {
        currentHealth = hp;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateInterfaceBorder();
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
        GetComponent<GameAmountCounter>().CountGame();
        UICanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
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
        Time.timeScale = 1; // Восстановление времени
        gameOverCanvas.SetActive(false);
        currentHealth = maxHealth; // Восстановление HP

        if (!useSave) // удаление сохранения, если игрок выбрал начать сначала
        {
            string path = Application.persistentDataPath + "/game_data.json";
            if (File.Exists(path)) File.Delete(path);
        }

        // Перезапуск сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuOnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
