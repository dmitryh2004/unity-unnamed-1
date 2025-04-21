using UnityEngine;

public class AntigravityBlock : MonoBehaviour
{
    [SerializeField] private float gravity = 9.81f;
    private Rigidbody rb;

    void Start()
    {
        // Получаем компонент Rigidbody и отключаем стандартную гравитацию
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
        }
        else
        {
            Debug.LogError("Rigidbody не найден!");
        }
    }

    // FixedUpdate вызывается на каждом шаге физического движка
    void FixedUpdate()
    {
        // Добавляем силу вверх, имитируя антигравитацию
        if (rb != null)
        {
            rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);
        }
    }
}
