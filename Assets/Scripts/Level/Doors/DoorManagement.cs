using UnityEngine;

public class DoorManagement : MonoBehaviour
{
    public bool open = false;
    DoorAudioPlayer audioPlayer;
    Animator anim;
    void Start()
    {
        audioPlayer = GetComponent<DoorAudioPlayer>();
        anim = GetComponent<Animator>();
        anim.SetBool("open", open);
    }

    public void OpenDoor()
    {
        open = true;
        anim.SetBool("open", true);
    }

    public void CloseDoor()
    {
        open = false;
        anim.SetBool("open", false);
    }

    public void ChangeState()
    {
        if (open) CloseDoor(); else OpenDoor();
        audioPlayer.PlayDoorSound();
    }
}
