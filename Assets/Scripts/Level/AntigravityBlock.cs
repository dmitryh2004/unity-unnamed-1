using UnityEngine;

public class AntigravityBlock : MonoBehaviour
{
    [SerializeField] float gravity = 9.81f;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.up * gravity);
    }
}
