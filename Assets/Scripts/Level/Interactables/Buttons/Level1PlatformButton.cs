using UnityEngine;

public class Level1PlatformButton : BaseButton
{
    public Transform platform;
    Animator platformAnim;

    void Start()
    {
        repressable = false;
        platformAnim = platform.GetComponent<Animator>();
    }

    public override void OnPressDownButton()
    {
        platformAnim.SetBool("activated", true);
    }

    public override void OnPressUpButton()
    {

    }
}
