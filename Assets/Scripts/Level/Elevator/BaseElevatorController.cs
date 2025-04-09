using System;
using UnityEngine;

public class BaseElevatorController : MonoBehaviour
{
    public int minFloor = 1, maxFloor = 2;
    public int startFloor = 1;
    int currentFloor;
    int destFloor;
    public Animator anim;
    public Transform leftDoor, rightDoor;
    Animator leftDoorAnimator;
    Animator rightDoorAnimator;
    bool doorState = false;
    bool moving = false;

    public void SetMoving(bool moving)
    {
        this.moving = moving;
    }

    public void SetFloor(int floor)
    {
        destFloor = Math.Clamp(floor, minFloor, maxFloor);
        anim.SetInteger("floor", destFloor);
    }

    public int GetCurrentFloor()
    {
        return currentFloor;
    }

    public int GetDestFloor()
    {
        return destFloor;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentFloor = minFloor;
        leftDoorAnimator = leftDoor.GetComponent<Animator>();
        rightDoorAnimator = rightDoor.GetComponent<Animator>();
        SetFloor(startFloor);
    }

    public void ChangeDoorState(bool opened)
    {
        if (doorState != opened)
        {
            leftDoorAnimator.SetBool("open", opened);
            rightDoorAnimator.SetBool("open", opened);
        }
        doorState = opened;
    }

    public void SetCurrentFloor(int newFloor)
    {
        currentFloor = newFloor;
        Debug.Log("Current floor: " + currentFloor + "; Destination floor: " + destFloor);
    }

    protected void CheckElevatorPosition()
    {
        if ((currentFloor == destFloor) && !moving)
        {
            ChangeDoorState(true);
        }
        else
        {
            ChangeDoorState(false);
        }
    }

    void Update()
    {
        CheckElevatorPosition();
    }
}
