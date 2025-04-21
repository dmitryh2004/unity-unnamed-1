using UnityEngine;

public class SpaceshipLever : BaseButton
{
    [SerializeField] Cinematic cinematic;
    private void Start()
    {
        repressable = false;
    }
    public override void OnPressDownButton()
    {
        cinematic.StartCinematic();
    }

    public override void OnPressUpButton()
    {

    }
}
