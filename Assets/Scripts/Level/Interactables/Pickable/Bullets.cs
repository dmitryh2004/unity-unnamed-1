using UnityEngine;

public class Bullets : Pickable
{
    [SerializeField] Inventory playerInventory;
    public override void OnPickup()
    {
        playerInventory.AddAmmo(3);
        Destroy(interactText);
    }

    protected override bool CheckConditionForUpdateHint()
    {
        return true;
    }
}
