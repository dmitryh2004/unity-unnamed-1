using UnityEngine;

public abstract class Pickable : Interactable
{
    public abstract void OnPickup();
    public void Pickup()
    {
        OnPickup();
        gameObject.SetActive(false);
    }
}
