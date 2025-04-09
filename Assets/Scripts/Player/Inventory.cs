using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int ammo;
    [SerializeField] TMP_Text bulletText;

    public int GetAmmoCount()
    {
        return ammo;
    }

    public void SetAmmoCount(int ammo)
    {
        this.ammo = ammo;
    }

    public void AddAmmo(int diff)
    {
        ammo += diff;
    }

    public void UseAmmo()
    {
        if (ammo > 0) ammo--;
    }

    private void Update()
    {
        bulletText.SetText("" + ammo);
    }
}
