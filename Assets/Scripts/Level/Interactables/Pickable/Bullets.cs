using UnityEngine;

public class Bullets : Pickable
{
    [SerializeField] Inventory playerInventory;
    [SerializeField] int ammoCount = 3;
    public override void OnPickup()
    {
        playerInventory.AddAmmo(ammoCount);
        Destroy(interactText);
    }

    protected override bool CheckConditionForUpdateHint()
    {
        return true;
    }
}
