using UnityEngine;

public class AntigravityBlock : MonoBehaviour
{
    [SerializeField] private float gravity = 9.81f;
    private Rigidbody rb;

    void Start()
    {
        // �������� ��������� Rigidbody � ��������� ����������� ����������
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
        }
        else
        {
            Debug.LogError("Rigidbody �� ������!");
        }
    }

    // FixedUpdate ���������� �� ������ ���� ����������� ������
    void FixedUpdate()
    {
        // ��������� ���� �����, �������� ��������������
        if (rb != null)
        {
            rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);
        }
    }
}
