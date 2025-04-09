using UnityEngine;

public class Heal : Pickable
{
    [SerializeField] int healAmount = 50;
    [SerializeField] PlayerHealth playerHealth;
    public override void OnPickup()
    {
        playerHealth.Heal(healAmount);
        Destroy(interactText);
    }

    protected override bool CheckConditionForUpdateHint()
    {
        return true;
    }
}
