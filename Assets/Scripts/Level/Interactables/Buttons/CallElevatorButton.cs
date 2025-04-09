using System.Collections;
using UnityEngine;

public class CallElevatorButton : BaseButton
{
    public int floor;
    public Transform elevator;
    BaseElevatorController elevatorController;
    void Start()
    {
        repressable = true;
        elevatorController = elevator.GetComponent<BaseElevatorController>();
    }

    public override void OnPressDownButton()
    {
        Debug.Log("Elevator is called on floor " + floor);
        elevatorController.SetFloor(floor);
        StartCoroutine(AutoPressUp());
    }

    private IEnumerator AutoPressUp()
    {
        yield return new WaitForSeconds(0.25f);
        Press();
    }

    public override void OnPressUpButton()
    {

    }
}
