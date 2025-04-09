using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public Transform player; // ������ �� ������ �������� ������
    PlayerHealth playerHealth;
    public TMP_Text healthText; // ������ �� Text

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();    
    }
    void Update()
    {
        // ��������� �����
        healthText.text = "HP: " + playerHealth.GetCurrentHealth() + " / " + playerHealth.maxHealth;
    }
}
