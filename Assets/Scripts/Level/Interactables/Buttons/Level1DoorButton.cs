using UnityEngine;
using System.Collections.Generic;

public class Level1DoorButton : BaseButton
{
    public List<Transform> doors;
    List<DoorManagement> doorManagers;

    void SetDoors()
    {
        foreach (Transform door in doors)
        {
            doorManagers.Add(door.GetComponent<DoorManagement>());
            Debug.Log(door.GetComponent<DoorManagement>());
        }
    }

    void Start()
    {
        doorManagers = new List<DoorManagement>();
        repressable = true;
        SetDoors();
    }

    public override void OnPressDownButton()
    {
        if (doorManagers.Count == 0) SetDoors();
        foreach(DoorManagement doorManager in doorManagers)
            doorManager.ChangeState();
    }

    public override void OnPressUpButton()
    {
        if (doorManagers.Count == 0) SetDoors();
        foreach (DoorManagement doorManager in doorManagers)
            doorManager.ChangeState();
    }
}
