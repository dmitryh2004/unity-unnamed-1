using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 3.0f;
    [SerializeField] Camera playerCamera;
    [SerializeField] Animator anim;
    PlayerAudioPlayer audioPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioPlayer = GetComponent<PlayerAudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactRange))
        {
            bool hasInteraction = true;
            bool interactableIsPickable = false;
            if (hit.collider.CompareTag("Button"))
            {
                hit.collider.GetComponent<BaseButton>().Press();
            }
            else if (hit.collider.CompareTag("Pickable"))
            {
                hit.collider.GetComponent<Pickable>().Pickup();
                interactableIsPickable = true;
            }
            else
            {
                hasInteraction = false;
            }
            if (hasInteraction)
            {
                anim.Play("interact");
                if (interactableIsPickable) audioPlayer.PlayPovezloSound();
                else audioPlayer.PlayInteractSound();
            }
        }
    }
}
