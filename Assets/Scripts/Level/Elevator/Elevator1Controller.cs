using UnityEngine;

public class Elevator1Controller : BaseElevatorController
{
    public bool trapIsActive = true;
    public Transform trappedFloor;
    void Update()
    {
        CheckElevatorPosition();

        if (trapIsActive && (GetCurrentFloor() == GetDestFloor()) && (GetDestFloor() == 2))
        {
            if (trappedFloor.gameObject.activeInHierarchy)
                trappedFloor.gameObject.SetActive(false);
        }
        else
        {
            if (!trappedFloor.gameObject.activeInHierarchy)
                trappedFloor.gameObject.SetActive(true);
        }
    }
}
