using UnityEngine;

public class Level1DisableTrapButton : BaseButton
{
    public Transform elevator;
    Elevator1Controller elevatorController;
    public override void OnPressDownButton()
    {
        Debug.Log("Elevator's trap disabled");
        elevatorController.trapIsActive = false;
    }

    public override void OnPressUpButton()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        repressable = false;
        elevatorController = elevator.GetComponent<Elevator1Controller>();
    }
}
